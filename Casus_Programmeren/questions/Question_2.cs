namespace Casus_Programmeren.questions;

public class Question_2
{
    public static void question2()
    {
        List<string> roomNumbers = new List<string>();

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
        
        helper.handleTerminal(roomNumbers, "Zuurstof berekenen", "Selecteer een Ruimte nummer");
    }
}