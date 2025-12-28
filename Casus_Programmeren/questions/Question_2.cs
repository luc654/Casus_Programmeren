namespace Casus_Programmeren.questions;

public class Question_2
{
    /// <summary>
    /// Calculates the oxygen use and remaining oxygen from a room.
    /// </summary>
    /// <remarks>
    /// Requires terminalHelper.
    /// Requires atleast one 'Room' in globalcontext.
    /// </remarks>
    public static void question2()
    {
        List<string> terugknop = new List<string>() { "terug" };
        List<string> roomNumbers = new List<string>();
        ;

        foreach (var room in Program.GlobalContext.Rooms.getRooms())
        {
            roomNumbers.Add(room.roomNumber.ToString());        
        }

        if (roomNumbers.Count == 0)
        {
            Program.GlobalContext.notification = "Geen ruimtes om te berekenen!";
            return;
        }
        
        terminalHelper helper = new terminalHelper();
        
        int selectedRoomINdex = helper.handleTerminal(roomNumbers, "Zuurstof berekenen", "Selecteer een Ruimte nummer");
        
        Room selectedRoom = Program.GlobalContext.Rooms.getRooms()[selectedRoomINdex];

        string collegeMinuten;
        string studentenAantal;
        
        do {
        collegeMinuten = helper.handleQuestion("Hoeveel minuten duurt de college");
        } while (validate(collegeMinuten));
        
        do {
            studentenAantal = helper.handleQuestion("Hoeveel studenten zijn aanwezig");
        } while (validate(studentenAantal));
        
        double hours = int.Parse(collegeMinuten) / 60;
        int studentsInt =  int.Parse(studentenAantal);
        
        
        OxygenResult data = selectedRoom.getOxygenUse(studentsInt, hours);

        string formattedData = $"""
                                Gebruikt zuurstof: {data.Used} liter
                                Overgebleven zuurstof: {data.Remaining} liter
                                Maximaal aantal uren: {data.MaxHours}
                                """;
        
        helper.handleTerminal(terugknop, "Zuurstof resultaat", formattedData);
        return;
    }
    
    
    private static bool validate(string value)
    {
        if (int.TryParse(value, out int output))
        {
            if(output <= 0)
            {
                Program.GlobalContext.notification = "Waarde moet een positief getal zijn";
                return true;
            }
            return false;
        }
        else
        {
            Program.GlobalContext.notification = "Waarde moet een heel getal zijn";
            return true;
        }
    }
}