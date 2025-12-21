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
}