namespace Casus_Programmeren;

public class Reservations
{
    public readonly List<Reservation> _reservations = new();

    public void addReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
    
    public List<Reservation> getReservations()
    {
        return _reservations;
    }
    public void LogAllReservations()
    {
        if (_reservations.Count == 0)
        {
            Console.WriteLine("Geen reserveringen gevonden.");
            Console.ReadLine();
            return;
        }

        foreach (var reservation in _reservations)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"UID:        {reservation.Uid}");
            Console.WriteLine($"Building:   {reservation.Building}");
            Console.WriteLine($"Room:       {reservation.roomNumber}");
            Console.WriteLine($"Location:   {reservation.Location}");
            Console.WriteLine($"Summary:    {reservation.Summary}");

            Console.WriteLine($"Start:      {reservation.Start:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"End:        {reservation.End:yyyy-MM-dd HH:mm}");

            Console.WriteLine("Attendees:");
            foreach (var attendee in reservation.Attendees)
            {
                Console.WriteLine($" - {attendee}");
            }
        }

        Console.WriteLine("--------------------------------------------------");
        Console.ReadLine();
    }

}