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
            "Automatisch lokaal reserveren",
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
                running = false;
                break;
        }
    }
}