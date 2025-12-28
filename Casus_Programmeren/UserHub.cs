using Casus_Programmeren.questions;

namespace Casus_Programmeren;

public class UserHub
{
    private static bool running = true;
    public static void loop()
    {
        List<string> options = new List<string>
        {
            "AI Powered Blockchain algorithmic Route Berekening (AI powered)",
            "Zuurstof berekenen",
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
                Question_2.question2();
                break;
            case 2:
                Console.WriteLine("Wednesday");
                break;
            case 3:
                Console.WriteLine("Thursday");
                break;
            case 4:
                Console.WriteLine("Friday");
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