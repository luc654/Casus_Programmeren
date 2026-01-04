using Casus_Programmeren.questions;

namespace Casus_Programmeren;

public class UserHub
{
    private static bool running = true;
    public static void loop()
    {
        
        running = true;
        List<string> options = new List<string>
        {
            "AI Powered Blockchain algorithmic Route Berekening (AI powered)",
            "Uitloggen"
        };


        while (running)
        {
         
            terminalHelper helper = new terminalHelper();
            int decision = helper.handleTerminal(options, "Studenten hub",
                "Selecteer een optie om de Casus te beginnen");
            
            flush(decision);
            

        } 
    }
    
    
    private static void flush(int decision)
    {
        switch (decision) 
        {
            case 0:
                Question_3.question3();
                break;
            case 1:
                running = false;
                break;
        }
    }
}