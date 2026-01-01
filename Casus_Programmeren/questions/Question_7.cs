using System.Globalization;

namespace Casus_Programmeren.questions;

public class Question_7
{
    private static terminalHelper helper = new terminalHelper();

    public static void reserveRoom()
    {
        // Definition of all Buttons.
        List<string> terugknop = new List<string>() { "terug" };

        Building selectedBuilding = getBuildingtype();
        
        Room room =  getRoom(selectedBuilding);
        if (room == null) return;

        string date = getDate();

        bool valideTime = false;

        ReservationHelper reservationHelper = new ReservationHelper();
        ReservationHelper.reservationTime selectedTime;
        List<ReservationHelper.reservationTime> reservedTime = reservationHelper.getReservedDates(room,date);
        do
        {

            selectedTime = reservationHelper.handleReservationInput(reservedTime);
            valideTime = reservationHelper.validateInput(selectedTime, reservedTime);
        } while (!valideTime);
        
        
        DateTimeOffset startTime = ToDateTimeOffset(date, selectedTime.startHours, selectedTime.startMinutes);
        DateTimeOffset endTime = ToDateTimeOffset(date, selectedTime.endHours, selectedTime.endMinutes);

        Reservation newReservation = new Reservation(reservationHelper.generateID(), startTime, endTime, "", [""], room.getFullName(),  selectedBuilding);
        Program.GlobalContext.Reservations.addReservation(newReservation);
    }


    private static Building getBuildingtype()
    {
        List<string> buildings = Enum.GetValues(typeof(Building))
            .Cast<Building>()
            .Select(b => b.ToString())
            .ToList();

        int selectedBuildingIndex = helper.handleTerminal(buildings, "Ruimte reserveren", "Selecteer een gebouw");
        string selectedBuilding = buildings[selectedBuildingIndex];

        
        // https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp
        Enum.TryParse(selectedBuilding, out Building enumBuilding);
        
        return enumBuilding;
    }

    private static Room? getRoom(Building building)
    {
        List<string> roomNumbers = new List<string>();
        
        
        foreach (var room in Program.GlobalContext.Rooms.getRooms())
        {
            if(room.building == building) roomNumbers.Add(room.roomNumber);        
        }

        if (roomNumbers.Count == 0)
        {
            Program.GlobalContext.notification = "Geen ruimtes om te berekenen!";
            return null;
        }

        int roomIndex =helper.handleTerminal(roomNumbers, "Ruimte reserveren", "Selecteer een ruimte");
        string roomNumber = roomNumbers[roomIndex];

        // Since roomNumbers only contains rooms from a specific building, we cant rely on the ID given from roomIndex. Due to this we have to determine the selectedroom by using roomNumber.  
        Room selectedRoom = Program.GlobalContext.Rooms.getRooms().Find(room => room.roomNumber == roomNumber && room.building == building);

        return selectedRoom;
    }

    private static string getDate()
    {
        string date = helper.handleQuestion("Voer begin datum in in format 'YYYY-MM-DD'");
        return date;
    }
    
    private static DateTimeOffset ToDateTimeOffset(
        string date,
        int hours,
        int minutes
    )
    {
        DateTime day = DateTime.ParseExact(
            date,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture
        );

        return new DateTimeOffset(
            day.Year,
            day.Month,
            day.Day,
            hours,
            minutes,
            0,
            TimeSpan.Zero
        );
    }


}