namespace Casus_Programmeren;

public class Rooms
{
    private readonly List<Room> _rooms = new();
    
    public void addRoom(Room room)
    {
        _rooms.Add(room);
    }
    
    public List<Room> getRooms()
    {
        return _rooms;
    }
    
    public void LogAllRooms()
    {
        if (_rooms.Count == 0)
        {
            Console.WriteLine("Geen reserveringen gevonden.");
            Console.ReadLine();
            return;
        }

        foreach (var room in _rooms)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"roomNumber: {room.roomNumber}");
            Console.WriteLine($"roomName:   {room.roomName}");
            Console.WriteLine($"roomVolume: {room.roomVolume}");
            Console.WriteLine($"Long:       {room.geoLocation.longitude}");
            Console.WriteLine($"Lat:        {room.geoLocation.latitude}");
            Console.WriteLine($"building:   {room.building}");
            Console.WriteLine($"roomType:   {room.roomType}");

        }

        Console.WriteLine("--------------------------------------------------");
        Console.ReadLine();
    }
}