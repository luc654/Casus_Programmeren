namespace Casus_Programmeren;

public class Reservation
{
    public string Uid { get; init; }

    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }

    public TimeZoneInfo TimeZone { get; init; }

    public string Location { get; init; }
    public string Summary { get; init; }

    public IReadOnlyList<string> Attendees { get; init; }

    public DateTimeOffset CreatedAt { get; init; }


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
