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
    
}