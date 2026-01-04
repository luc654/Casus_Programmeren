using System.Globalization;

namespace Casus_Programmeren.questions;

public class Question_10
{
    private static terminalHelper helper = new terminalHelper();
    private static ReservationHelper _reservationHelper = new ReservationHelper();


    public static void AutomaticPlanner()
    {

        helper.setTitle("Automatisch plannen");
        helper.setDescription("Vul informatie in.");

        string date = getDate();
        int requestedOccupacy = getRequestedOccupancy();
        Room room;
        do
        {
            room = getRoom();

        } while (capacityCheck(room, requestedOccupacy));
            
        ReservationHelper.reservationTime requestedTime = getRequestedTime();


        // second request a room 

        List<ReservationHelper.reservationTime> reservedTime = _reservationHelper.getReservedDates(room, date);

        bool roomPossible = _reservationHelper.validateInput(requestedTime, reservedTime);

        // If the selected room is available on the selected time, make reservation and return
        if (roomPossible)
        {
            makeReservation(date, requestedTime, room, room.building);
            Program.GlobalContext.notification = $"Ruimte {room.getFullName()} gereserveerd";
            return;
        }
        else
        {

            // If selected room is not avialable, generate a list of rooms with enough capacity, sorted ascending on the capacity.
            List<Room> otherRooms = getOtherRooms(requestedOccupacy);
            foreach (Room otherRoom in otherRooms)
            {
                List<ReservationHelper.reservationTime> reserveTime = _reservationHelper.getReservedDates(otherRoom, date);

                bool otherRoomPossible = _reservationHelper.validateInput(requestedTime, reserveTime);

                if (otherRoomPossible)
                {
                    makeReservation(date, requestedTime, otherRoom, otherRoom.building);
                    Program.GlobalContext.notification = $"Aanvraagde ruimte niet beschikbaar. ruimte {otherRoom.getFullName()} gereserveerd ({otherRoom.capacity} plekken)";
                    return;
                }
            }
            Program.GlobalContext.notification = $"Geen ruimtes kunnen vinden met {requestedOccupacy} plekken...";
            return;

        }

    }

    private static void makeReservation(string date, ReservationHelper.reservationTime selectedTime, Room room, Building selectedBuilding)
    {
        DateTimeOffset startTime = ToDateTimeOffset(date, selectedTime.startHours, selectedTime.startMinutes);
        DateTimeOffset endTime = ToDateTimeOffset(date, selectedTime.endHours, selectedTime.endMinutes);

        Reservation newReservation = new Reservation(_reservationHelper.generateID(), startTime, endTime, "", [""], room.getFullName(),  selectedBuilding);
        Program.GlobalContext.Reservations.addReservation(newReservation);
    }

    private static bool capacityCheck(Room room, int capacity)
    {
        if (room.capacity < capacity)
        {
            Program.GlobalContext.notification = $"Ruimte {room.getFullName()} heeft niet {capacity} plekken";
            return true;
        }
        else
        {
            return false;
        }
    }
private static int getRequestedOccupancy()
    {
        string input;
        do
        {
            input = helper.handleQuestion("Hoeveel mensen moeten in het lokaal passen?");

        } while (validate(input));
        return int.Parse(input);
    }

    private static ReservationHelper.reservationTime getRequestedTime()
    {
        ReservationHelper.reservationTime requestedTime = _reservationHelper.handleReservationInput(null);
        return requestedTime;
    }

    private static Room getRoom()
    {
        Building buildingType = getBuildingtype();
        
        string room;
        do
        {
            room = helper.handleQuestion("Voer Start ruimte nummer in.");
        } while (validateRoom(room, buildingType));
    
        Room selectedRoom = Program.GlobalContext.Rooms.getRooms().Find(x => x.roomNumber == room && x.building == buildingType);
            
        return selectedRoom;
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
    
    private static Building getBuildingtype()
    {
        List<string> buildings = Enum.GetValues(typeof(Building))
            .Cast<Building>()
            .Select(b => b.ToString())
            .ToList();

        int selectedBuildingIndex = helper.handleTerminal(buildings, "Automatisch plannen", "Selecteer een gebouw");
        string selectedBuilding = buildings[selectedBuildingIndex];

        
        // https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp
        Enum.TryParse(selectedBuilding, out Building enumBuilding);
        
        return enumBuilding;
    }
    
    private static bool validateRoom(string input, Building buildingType)
    {
        int index = Program.GlobalContext.Rooms.getRooms().FindIndex(room => room.roomNumber == input  && room.building == buildingType);

        if (index == -1)
        {
            Program.GlobalContext.notification = $"Geen kamer gevonden met nummer {input}";
            return true;
        }
        else
        {
            return false;
        }
    }
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

    private static List<Room> getOtherRooms(int minCapacity)
    {
        List<Room> allRooms = Program.GlobalContext.Rooms.getRooms();

        List<Room> validRooms = new();
        foreach (var room in allRooms)
        {
            if (room.capacity > minCapacity)
            {
                validRooms.Add(room);
            }
        }

        return validRooms.OrderBy(r => r.capacity).ToList();

    }
}