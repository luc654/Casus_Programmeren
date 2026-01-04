using System.Globalization;

namespace Casus_Programmeren;

public class ReservationHelper
{
    /// <summary>
    /// Takes a Room and a string date in format YYYY-MM-DD.
    /// Returns a list of reservationTimes.
    /// </summary>
    /// <param name="room"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public List<reservationTime> getReservedDates(Room room, string date)
    {
        DateTime targetDate = DateTime.ParseExact(
            date,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture
        );

        List<Reservation> allReservations =
            Program.GlobalContext.Reservations
                .getReservations()
                .FindAll(r =>
                    r.roomNumber.Trim() == room.roomNumber.Trim() &&
                    r.Start.Date == targetDate.Date
                );

        List<reservationTime> reservedDates = new();

        foreach (var reservation in allReservations)
        {
            reservedDates.Add(
                new reservationTime(
                    reservation.Start.Hour,
                    reservation.End.Hour,
                    reservation.Start.Minute,
                    reservation.End.Minute
                )
            );
        }


        return reservedDates; 
    }




/// <summary>
/// Inputs a reservationTime of your current attempted Reservation and a list of reservationtimes of already active reservation on that day. Return True if its possible to make the reservation and false if not possible.
/// </summary>
/// <param name="inputtedTime"></param>
/// <param name="reservedDates"></param>
/// <returns></returns>
    public bool validateInput(reservationTime inputtedTime, List<reservationTime> reservedDates)
    {
        int inputStart = inputtedTime.startHours * 60 + inputtedTime.startMinutes;
        int inputEnd   = inputtedTime.endHours * 60 + inputtedTime.endMinutes;

        if (reservedDates != null)
        {
            
        foreach (var reserved in reservedDates)
        {
            int reservedStart = reserved.startHours * 60 + reserved.startMinutes;
            int reservedEnd   = reserved.endHours * 60 + reserved.endMinutes;

            // Return false if inputted date overlaps.
            if (inputStart < reservedEnd && inputEnd > reservedStart)
            {
                Program.GlobalContext.notification = "Selecteer andere tijd";
                return false; 
            }
        }
        }

        return true; 
    }

    public reservationTime handleReservationInput(List<reservationTime> reservedDates)
    {
        terminalHelper helper = new terminalHelper();

        string questionOne = "Selecteer een begintijd als 'HH:MM'";
        string questionTwo = "Selecteer een eindtijd als 'HH:MM'";


        if (reservedDates != null && reservedDates.Count > 0)
        {
            
        
        if (reservedDates.Count > 1)
        {
            for (int i = 0; i < reservedDates.Count - 1; i++)
            {
                questionOne += Environment.NewLine +
                               $"Tussen {reservedDates[i].endHours:D2}:{reservedDates[i].endMinutes:D2} " +
                               $"en {reservedDates[i + 1].startHours:D2}:{reservedDates[i + 1].startMinutes:D2}";

                questionTwo += Environment.NewLine +
                               $"Tussen {reservedDates[i].endHours:D2}:{reservedDates[i].endMinutes:D2} " +
                               $"en {reservedDates[i + 1].startHours:D2}:{reservedDates[i + 1].startMinutes:D2}";
            }
        }
        else if (reservedDates.Count == 1)
        {
            var r = reservedDates[0];
            questionOne += Environment.NewLine +
                           $"Tussen 00:00 en {r.startHours:D2}:{r.startMinutes:D2}" +
                           Environment.NewLine +
                           $"Tussen {r.endHours:D2}:{r.endMinutes:D2} en 23:59";

            questionTwo += Environment.NewLine +
                           $"Tussen 00:00 en {r.startHours:D2}:{r.startMinutes:D2}" +
                           Environment.NewLine +
                           $"Tussen {r.endHours:D2}:{r.endMinutes:D2} en 23:59";
        }
        }

        
        string startTime = helper.handleQuestion(questionOne);
        string endTime = helper.handleQuestion(questionTwo);
        
        if(startTime.EndsWith("00"))
        {
            startTime = startTime.Substring(0, startTime.Length - 1);
        }
        
        if(endTime.EndsWith("00"))
        {
            endTime = endTime.Substring(0, endTime.Length - 1);
        }

        int startMinutes = int.Parse(startTime.Split(":")[1]);
        int startHours = int.Parse(startTime.Split(":")[0]);
        
        int endHours = int.Parse(endTime.Split(":")[0]);
        int endMinutes = int.Parse(endTime.Split(":")[1]);
        
        reservationTime time = new reservationTime(startHours, endHours, startMinutes, endMinutes);
        return time;
    }

    public class reservationTime
    {
        public int startHours {get;} 
        public int endHours {get;} 
        public int startMinutes {get;} 
        public int endMinutes {get;}

        public reservationTime(int startHours, int endHours, int startMinutes, int endMinutes)
        {
            this.startHours = startHours;
            this.endHours = endHours;
            this.startMinutes = startMinutes;
            this.endMinutes = endMinutes;
            
        }
    }
    
    public string generateID()
    {
        return Guid.NewGuid().ToString("N");
    }

    public int getCurrentOccupants(string date, int hour, Building? building = null)
    {
        List<Reservation> reservations = getReservationsFromDate(date);
        List<Reservation> currentHourReservations = retrieveHourspecificReservations(reservations, hour);
        List<Room> rooms = reservations2rooms(currentHourReservations);
        List<Room> filteredRooms = new();
        if (building != null)
        {
            filteredRooms = filterBuilding(rooms, building);
        }
        else
        {
            filteredRooms = rooms;
        }
        int totalOccupants = countOccupants(filteredRooms);
        return totalOccupants;


    }

/// <summary>
/// Inputs a yyyy-mm-dd date and returns a List of Reservations with START DATE the same as inputted date.
/// </summary>
/// <param name="date"></param>
/// <returns></returns>
    public List<Reservation> getReservationsFromDate(string date)
    {
        
        DateTime targetDate = DateTime.ParseExact(
            date,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture
        );
        
        
        List<Reservation> allReservations = Program.GlobalContext.Reservations.getReservations();
        List<Reservation> filteredReservations = allReservations.FindAll(r =>
            r.Start.Date == targetDate.Date
        );
        
        return filteredReservations;
    }

/// <summary>
/// Inputs a list of reservations and an integer Hour, returns a List of Reservations with start hour the same as inputted hour
/// </summary>
/// <param name="reservations"></param>
/// <param name="hour"></param>
/// <returns></returns>
    private List<Reservation> retrieveHourspecificReservations(List<Reservation> reservations, int hour)
    {
        
        List<Reservation> filteredReservations = reservations.FindAll(r =>
            r.Start.Hour == hour
        );
        return filteredReservations;
    }

/// <summary>
/// Inputs a list of reservations, returns a list of rooms. rooms may appear double.
/// </summary>
/// <param name="reservations"></param>
/// <returns></returns>
    private List<Room> reservations2rooms(List<Reservation> reservations)
    {
        List<Room> allRooms = Program.GlobalContext.Rooms.getRooms();
        List<Room> filteredRooms = new();
        foreach (Reservation reservation in reservations)
        {
            Room matchedRoom = allRooms.Find(r =>
                r.roomNumber == reservation.roomNumber && r.building == reservation.Building);
            filteredRooms.Add(matchedRoom);
        }
        return filteredRooms;
    }

/// <summary>
/// Inputs a list of rooms, counts each room and returns sum of capacity.
/// </summary>
/// <param name="rooms"></param>
/// <returns></returns>
    private int countOccupants(List<Room> rooms)
    {
        int totalOccupants = 0;
        foreach (Room room in rooms)
        {
            totalOccupants += room.capacity;
        }
        return totalOccupants;
    }

/// <summary>
/// Inputs a list of rooms and a Building building, returns a list of rooms where building is building
/// </summary>
/// <param name="rooms"></param>
/// <param name="building"></param>
/// <returns></returns>
private List<Room> filterBuilding(List<Room> rooms, Building? building)
{
    if (rooms == null)
        return new List<Room>();

    return rooms.FindAll(r => r != null && r.building == building);
}

}

