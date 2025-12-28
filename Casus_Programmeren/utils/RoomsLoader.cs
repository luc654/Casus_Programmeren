using System.Text.Json;
using System.Text.Json.Serialization;

namespace Casus_Programmeren;

public class RoomContainer
{
    public List<Room> rooms { get; set; }
}
public class RoomsLoader
{
    public void loadRooms()
    {
        // Yes, a part of this code has been generated, yes i understand my own hypocrisy, no i dont care. this course emphasizes the use of generative AI with suchs extends that the art of coding has already been lost
        string projectRoot = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string jsonFilePath = Path.Combine(projectRoot, "data", "rooms.json");
    
        if (!File.Exists(jsonFilePath)) return;

        string jsonContent = File.ReadAllText(jsonFilePath);

        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        JsonElement root = doc.RootElement;
        JsonElement roomsArray = root.GetProperty("rooms");

        List<Room> loadedRooms = new List<Room>();

        foreach (JsonElement element in roomsArray.EnumerateArray())
        {
            string rNumber = element.GetProperty("roomNumber").GetString();
            string rName = element.GetProperty("roomName").GetString();
            float rVolume = (float)element.GetProperty("roomVolume").GetDouble();
            int rCap = element.GetProperty("capacity").GetInt32();
        
            JsonElement geo = element.GetProperty("geoLocation");

            Enum.TryParse(element.GetProperty("building").GetString(), out Building bldg);

            Room newRoom = new Room 
            ( 
                rNumber,
                rName,
                rVolume,
                rCap,
                geo.GetProperty("latitude").GetDouble(),
                geo.GetProperty("longitude").GetDouble(),
                bldg
            );

            loadedRooms.Add(newRoom);
            Program.GlobalContext.Rooms.addRoom(newRoom);
        }

        Program.GlobalContext.notification = $"{loadedRooms.Count} Kamers ingeladen!";
    }
}