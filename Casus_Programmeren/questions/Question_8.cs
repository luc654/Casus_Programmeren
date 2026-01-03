namespace Casus_Programmeren.questions;

public class Question_8
{
    
    private static terminalHelper helper = new terminalHelper();
    
    
    public static void seeOccupants()
    {
        List<string> terugknop = new List<string>() { "terug" };

        ReservationHelper reservationHelper = new ReservationHelper();
        Building building = getBuildingtype();
        string date = getDate();
        int hour = getHour();
        int presention = reservationHelper.getCurrentOccupants(date, hour, building);


        string formattedText = $"""
                                Gebouw {building} bevat op {date} om {hour} uur ongeveer {presention} mensen.
                                
                                *Mogelijk afwijkend met een margin van 100%
                                *Casus programmeren is niet aanspraakelijk voor verkeerde informatie
                                """;
        
        helper.handleTerminal(terugknop, "Aanwezigheid resultaat", formattedText);

    }
    
    private static Building getBuildingtype()
    {
        List<string> buildings = Enum.GetValues(typeof(Building))
            .Cast<Building>()
            .Select(b => b.ToString())
            .ToList();

        int selectedBuildingIndex = helper.handleTerminal(buildings, "Aanwezigheid berekenen", "Selecteer een gebouw");
        string selectedBuilding = buildings[selectedBuildingIndex];

        
        // https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp
        Enum.TryParse(selectedBuilding, out Building enumBuilding);
        
        return enumBuilding;
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
    private static int getHour()
    {
        string capacity;
        do
        {
            capacity = helper.handleQuestion("Van welk uur wilt u de aanwezigheid berekenen?");
        } while (validate(capacity));
        
        return int.Parse(capacity);
    }
    private static string getDate()
    {
        bool nonValidetime = true;
        string date;
        do
        {

            date = helper.handleQuestion("Voer begin datum in in format 'YYYY-MM-DD'");
            int hyphenCount = date.Count(x => x == '-');
            if (hyphenCount == 2)
            {
                nonValidetime = false;
            }
            else
            {
                Program.GlobalContext.notification = "Voer een valide datum in";
            }
        } while (nonValidetime);
        
        return date;
    }
}