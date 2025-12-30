using System.Runtime.InteropServices.Swift;
using System.Text.Json.Serialization;

namespace Casus_Programmeren;



public readonly struct GeoLocation
{
    [JsonConstructor]
    public GeoLocation(double latitude, double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public double latitude { get; init; }
    public double longitude { get; init; }

}

public class OxygenResult
{
    public double Used { get; set; }
    public double Remaining { get; set; }
    public double MaxHours { get; set; }
}

public enum Building
{
    Spectrum,
    Prisma
}

public enum Roomtype
{
    Lokaal,
    Werkruimte,
    Publiekeruimte
}
public class Room
{
    
    public string roomNumber { get; init; }

    public string roomName { get; init; }

    public float roomVolume { get; init; }

    public int capacity { get; init; }

    public GeoLocation geoLocation { get; init; }

    public Building building { get; init; }
    
    public Roomtype roomType { get; init; }
    
    
    public Room(string roomNumber, string roomName, float roomVolume, int capacity, double latitude, double longitude,  Building building, Roomtype roomtype=Roomtype.Werkruimte)
    {
        
        this.roomNumber = roomNumber;
        this.roomName = roomName;
        this.roomVolume = roomVolume;
        this.capacity = capacity;
        this.geoLocation = new GeoLocation(latitude, longitude);
        this.building = building;
        this.roomType = roomtype;
    }

    public string getFullName()
    {
        return $"{building.ToString()} - {roomNumber}";
    }

    /// <summary>
    /// Get total amount of oxygen used, Hours remaining and max hours of oxygen supply based on room volume (local), amount of people and duration of hours (external).
    /// </summary>
    /// <param name="people"></param>
    /// <param name="hours"></param>
    /// <returns>OxygenResult class</returns>
    public OxygenResult getOxygenUse(int people, double hours)
    {
        double idleOxygenHour = 30;
        double oxygenInRoom = getOxygenInRoom();
        
        double oxygenUsed  = oxygenUse(people, hours, idleOxygenHour);
        double oxygenRemaining = oxygenInRoom - oxygenUsed;
        double maxHours = maxHoursOfOxygen(oxygenInRoom, people * idleOxygenHour);
        
        return new OxygenResult
        {
            Used = oxygenUsed,
            Remaining = oxygenRemaining,
            MaxHours = maxHours
        };
    }


    private double getOxygenInRoom()
    {
        return roomVolume * .21 * 1000; 
    }

    private double oxygenUse(int  people,  double hours, double idleOxygenHour)
    {
        return idleOxygenHour * people * hours;
    }
    
    private double maxHoursOfOxygen(double oxygenInRoom, double oxygenUse)
    {
        return oxygenInRoom /  oxygenUse;
    }
}