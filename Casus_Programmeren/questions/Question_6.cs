namespace Casus_Programmeren.questions;

public class Question_6
{
    
    // Als beheerder wil ik de totale kosten en opbrengsten voor een verhuuraanvraag van de
    // overheid uitgerekend hebben.
    
    //  Lokaal in Spectrum: huurprijs = capaciteit * 20
    // • Lokaal in Prisma: huurprijs = capaciteit * 17,50
    // • Werkruimte in Spectrum: huurprijs = 120
    // • Werkruimte in Prisma: huurprijs = 150
    // • Publieke ruimte: huurprijs = 250
    // • Vanwege de lage bezetting is de huurprijs op vrijdag 20% lager (voor alle ruimtes)
    
    private static terminalHelper helper = new terminalHelper();

    public static void calculateRental()
    {
        // Definition of all Buttons.
        List<string> terugknop = new List<string>() { "terug" };
        List<string> dagen = new List<string>() { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag" };
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
        int selectedRoomIndex = helper.handleTerminal(roomNumbers, "Huuraanvraag berekenen", "Selecteer een Ruimte nummer");
        Room selectedRoom = Program.GlobalContext.Rooms.getRooms()[selectedRoomIndex];
        
        // Get day
        int selectedDayIndex = helper.handleTerminal(dagen, "Huuraanvraag berekenen", "Selecteer de dag");
        string day = dagen[selectedDayIndex];
        
        float costs = Equations.calculateRental(selectedRoom, day);
        
        string formattedData = $"""
                                Totale huurprijs voor kamer {selectedRoom.getFullName()} is {costs} euro 
                                """;
        
        helper.handleTerminal(terugknop, "Huren resultaat", formattedData);

    }
    
    
}