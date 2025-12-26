using System.Runtime.InteropServices.Swift;

namespace Casus_Programmeren;



public readonly struct GeoLocation
{
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


public class Room
{
    
    private int roomNumber;
    private string roomName;
    private float roomVolume;
    private int capacity;
    private GeoLocation geoLocation;
    
    public Room(int  roomNumber, string roomName, float roomVolume, int capacity, double latitude, double longitude)
    {
        
        this.roomNumber = roomNumber;
        this.roomName = roomName;
        this.roomVolume = roomVolume;
        this.capacity = capacity;
        this.geoLocation = new GeoLocation(latitude, longitude);
    }


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