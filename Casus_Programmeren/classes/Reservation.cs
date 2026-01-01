namespace Casus_Programmeren;

public class Reservation
{
    public string Uid { get; }
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    public string Summary { get; }
    public string Location { get; }
    
    public string roomNumber { get; }
    
    public Building Building { get; }
    public IReadOnlyList<string> Attendees { get; }

    public Reservation(
        string uid,
        DateTimeOffset start,
        DateTimeOffset end,
        string summary,
        IReadOnlyList<string> attendees,
        string location,
        Building building)
    {
        Uid = uid;
        Start = start;
        End = end;
        Summary = summary;
        Attendees = attendees;
        Location = location;
        Building = building;
        roomNumber = Location.Split(' ').Last();
    }
    
    
    public TimeParts getTime()
    {
        return new TimeParts
        {
            StartHour = Start.Hour,
            StartMinute = Start.Minute,
            EndHour = End.Hour,
            EndMinute = End.Minute
        };
    }
}


// Sealed prevents this class from being inherited. 
public sealed class TimeParts
{
    public int StartHour { get; init; }
    public int StartMinute { get; init; }
    public int EndHour { get; init; }
    public int EndMinute { get; init; }
}
