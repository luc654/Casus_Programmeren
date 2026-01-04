using System.IO.Pipelines;
using Casus_Programmeren.questions;

namespace Casus_Programmeren;

public class AdminHub
{
    private static bool running = true;
    public static void loop()
    {
        
        running = true;
        List<string> options = new List<string>
        {
            "Ruimte toevoegen",
            "Zuurstof berekenen",
            "Rekenregels aanpassen",
            "Kosten van ruimte berekenen",
            "Verhuuraanvraag berekenen",
            "Aanwezigheid bekijken",
            "bezettingsgraad berekenen",
            "Uitloggen"
        };


        while (running)
        {
         
            terminalHelper helper = new terminalHelper();
            int decision = helper.handleTerminal(options, "Admin hub",
                "Selecteer een optie om de Casus te beginnen");
            
            flush(decision);
            

        } 
    }
    
    
    private static void flush(int decision)
    {
        switch (decision) 
        {
            case 0:
                question_1.addRoom();
                break;
            case 1:
                Question_2.question2();
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
                Question_8.seeOccupants();
                break;
            case 6:
                Question_9.calculateOccupancyRate();
                break;
            case 7:
                running = false;
                break;
        }
    }
}