using System.Globalization;
using System.Text.RegularExpressions;

namespace Casus_Programmeren.questions;

public class Question_7
{
    private static terminalHelper helper = new terminalHelper();

    public static void reserveRoom()
    {
        Building selectedBuilding = getBuildingtype();

        int targetCapacity = getCapacity();
        Room room =  getRoom(selectedBuilding, targetCapacity);
        if (room == null) return;

        string date = getDate();

        bool valideTime;

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

/// <summary>
/// Asks the user for a type of building based on the Buildings enum. Returns a Building
/// </summary>
/// <returns></returns>
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

    /// <summary>
    /// Asks the user for the capacity of the room. Returns an integer
    /// </summary>
    /// <returns></returns>
    private static int getCapacity()
    {
        string capacity;
        do
        {
            capacity = helper.handleQuestion("Wat moet de capaciteit van de ruimte zijn?");
        } while (validate(capacity));
        
        return int.Parse(capacity);
    }
    
    private static Room? getRoom(Building building, int targetCapacity)
    {
        List<string> roomNumbers = new List<string>();
        
        
        foreach (var room in Program.GlobalContext.Rooms.getRooms())
        {
            if(room.building == building && room.capacity == targetCapacity) roomNumbers.Add(room.roomNumber);        
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

    
    /// <summary>
    /// Asks the user for a date in YYYY-MM-DD format. Returns this date as a string.
    /// </summary>
    /// <returns></returns>
    private static string getDate()
    {
        bool nonValidetime = true;
        string date;
        do
        {

            date = helper.handleQuestion("Voer begin datum in in format 'YYYY-MM-DD'");
            int hyphenCount = date.Count(x => x == '-');
            if (hyphenCount == 2)
            {
                nonValidetime = false;
            }
            else
            {
                Program.GlobalContext.notification = "Voer een valide datum in";
            }
        } while (nonValidetime);
        
        return date;
    }
    
    /// <summary>
    /// Transforms static data into datetimeoffset variable.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="hours"></param>
    /// <param name="minutes"></param>
    /// <returns></returns>
    private static DateTimeOffset ToDateTimeOffset( string date, int hours, int minutes)
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

    private static bool validate(string value)
    {
        if (int.TryParse(value, out int output))
        {
            if(output <= 0)
            {
                Program.GlobalContext.notification = "Waarde moet een positief getal zijn";
                return true;
            }
            return false;
        }
        else
        {
            Program.GlobalContext.notification = "Waarde moet een heel getal zijn";
            return true;
        }
    }
}