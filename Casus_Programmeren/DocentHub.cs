using Casus_Programmeren.questions;


namespace Casus_Programmeren;

public class DocentHub
{
   
    
    private static bool running = true;
    public static void loop()
    {
        
        running = true;
        List<string> options = new List<string>
        {
            "Ruimte reserveren",
            "Show all reservations",
            "Rekenregels aanpassen",
            "Kosten van ruimte berekenen",
            "Verhuuraanvraag berekenen",
            "Spectrum aanwezigheid",
            "bezettingsgraad inzage",
            "Uitloggen"
        };


        while (running)
        {
         
            terminalHelper helper = new terminalHelper();
            int decision = helper.handleTerminal(options, "Docent hub",
                "Selecteer een optie om de Casus te beginnen");
            
            flush(decision);
            

        } 
    }

    
        
    private static void flush(int decision)
    {
        switch (decision) 
        {
            case 0:
                Question_7.reserveRoom();
                break;
            case 1:
               Program.GlobalContext.Reservations.LogAllReservations();
                break;
            case 2:
                Console.WriteLine("Wednesday");
                break;
            case 3:
                question_5.quest5();
                break;
            case 4:
                Question_6.calculateRental();
                break;
            case 5:
                Console.WriteLine("Saturday");
                break;
            case 6:
                Console.WriteLine("Saturday");
                break;
            case 7:
                running = false;
                break;
        }
    }
}