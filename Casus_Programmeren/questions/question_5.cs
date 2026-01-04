namespace Casus_Programmeren.questions;


public class question_5
{
    private static terminalHelper helper = new terminalHelper();
    
    /// <summary>
    /// Calculate room cost on day or week basis.
    /// </summary>
    public static void quest5()
    {
        
        // Definition of all Buttons.
        List<string> terugknop = new List<string>() { "terug" };
        List<string> berekenBasis = new List<string>() { "Dag basis", "Week basis" };
        List<string> roomNumbers = new List<string>();
        
        
        foreach (var room in Program.GlobalContext.Rooms.getRooms())
        {
            roomNumbers.Add(room.getFullName());        
        }
        
        if (roomNumbers.Count == 0)
        {
            Program.GlobalContext.notification = "Geen ruimtes om te berekenen!";
            return;
        }
        
        
        // Get room
        int selectedRoomIndex = helper.handleTerminal(roomNumbers, "Kosten berekenen", "Selecteer een Ruimte nummer");
        Room selectedRoom = Program.GlobalContext.Rooms.getRooms()[selectedRoomIndex];

        // Get day or week rate
        int selecterBasisIndex = helper.handleTerminal(berekenBasis, "Kosten berekenen", "Selecteer een bereken basis");
        string basis = berekenBasis[selecterBasisIndex];
        bool dayBasis = basis.Equals("Dag basis");
        
        
        // Get start hour
        int startHour = getStarthour();

        float costs = Equations.calculateCost(selectedRoom, dayBasis, startHour);

        string formattedData = $"""
                                Totale prijs voor kamer {selectedRoom.getFullName()} is '{costs}' 
                                
                                Deze berekening is op {basis} berekent en heeft als startuur {startHour}
                                """;
        
        helper.handleTerminal(terugknop, "Kosten resultaat", formattedData);


    }
    

    private static int getStarthour()
    {
        string beginTime;
        do
        {
            beginTime = helper.handleQuestion("Hoelaat begint de reservatie?");
        } while(validate(beginTime));
        
        return int.Parse(beginTime);
         
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