namespace Casus_Programmeren.questions;

public class question_1
{
    public static void addRoom()
    {
        // Since handleQuestion only returns strings, we need to define each variable as string and turn them over to int / float later along the way.
        
        string naam = "";
        string roomNumber = "";
        string roomVolume = "";
        string capacity = "";
        string longitude = "0";
        string latitude = "0";
        
         // Get input values    
         terminalHelper helper = new terminalHelper();
         helper.setTitle("Ruimte toevoegen");
         helper.setDescription("Voer data in om nieuwe ruimte te maken...");

         
         
         naam =  helper.handleQuestion("Naam van ruimte");
         roomNumber =  helper.handleQuestion("Nummer van ruimte");
         
         do {
              roomVolume = helper.handleQuestion("Ruimte inhoud in m3:");
         } while (validateRoomVolume(roomVolume));

         do {
             capacity = helper.handleQuestion("Ruimte capaciteit:");
         } while (validateCapacity(capacity));
         do {
             longitude = helper.handleQuestion("Longitude (optioneel):", true);
         } while (validateLongitude(longitude));
         do {
             latitude = helper.handleQuestion("Latitude (optioneel):", true);
         } while (validateLatitude(latitude));


        // Source - https://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c
        // Posted by Jake Pearson, modified by community. See post 'Timeline' for change history
        // Retrieved 2025-12-26, License - CC BY-SA 3.0
        List<string> buildings = Enum.GetValues(typeof(Building))
            .Cast<Building>()
            .Select(b => b.ToString())
            .ToList();

        int buildingNumber = helper.handleTerminal(buildings);

        Building building = (Building)Enum.Parse(typeof(Building), buildings[buildingNumber]);

         
         Room room = new Room(roomNumber, naam,  float.Parse(roomVolume), int.Parse(capacity), Double.Parse(latitude), Double.Parse(longitude), building);
         Program.GlobalContext.Rooms.addRoom(room);
         
    }


    private static bool validateRoomVolume(string roomVolume)
    {
        if (float.TryParse(roomVolume, out float output))
        {
            if(output <= 0)
            {
                Program.GlobalContext.notification = "Volume moet een positief getal zijn";
                return true;
            }
            return false;
        }
        else
        {
            Program.GlobalContext.notification = "Volume moet een getal zijn";
            return true;
        }
    }

    private static bool validateCapacity(string capacity)
    {
        if (int.TryParse(capacity, out int output))
        {
            if(output <= 0)
            {
                Program.GlobalContext.notification = "Capaciteit moet een positief getal zijn";
                return true;
            }
            return false;
        }
        else
        {
            Program.GlobalContext.notification = "Waarde moet een getal zijn";
            return true;
        }
    }

    private static bool validateLongitude(string longitude)
    {
        if (string.IsNullOrEmpty(longitude))
        {
            return false;
        }
        if (double.TryParse(longitude, out double output))
        {
            if (output < -180 || output > 180)
            {
                Program.GlobalContext.notification = "Longitude is tussen -180 en 180";
                return true;    
            }
            return false;
        }
        else
        {
            Program.GlobalContext.notification = "Waarde moet een getal zijn";
            return true;
        }
    }
    
    private static bool validateLatitude(string latitude)
    {
        if (string.IsNullOrEmpty(latitude))
        {
            return false;
        }
        if (double.TryParse(latitude, out double output))
        {
            if (output < -90 || output > 90)
            {
                Program.GlobalContext.notification = "Latitude is tussen -90 en 90";
                return true;    
            }
            return false;
        }
        else
        {
            Program.GlobalContext.notification = "Waarde moet een getal zijn";
            return true;
        }
    }
}