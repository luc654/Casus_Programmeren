using System;
using Casus_Programmeren;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        while (true)
        {
            
        
        List<string> options = new List<string>
        {
            "Als student verder gaan",
            "Als beheerder verder gaan",
            "Data inladen",
        };

        terminalHelper helper = new terminalHelper();
     

        
        
         


        switch (helper.handleTerminal(options, "Casus Programmeren", "Selecteer een optie om de Casus te beginnen"))
        {
            case 0:
            {
                GlobalContext.CurrentUserRole = UserRole.Student;
                Hub hub = new Hub();
                break;
            }

            case 1:
            {
                GlobalContext.CurrentUserRole = UserRole.Admin;
                Hub hub = new Hub();
                break;
            }

            case 2:
            {
                DataLoader dataLoader = new DataLoader();
                dataLoader.Load();
                break;
            }

            default:
                Console.WriteLine("Ongeldige keuze.");
                break;
        }
        }
        
    }


    public enum UserRole
    {
        Student,
        Admin
    }

    /// <summary>
    /// Global variables holder.
    /// </summary>
    public static class GlobalContext
    {
        public static UserRole CurrentUserRole { get; set; }
        public static Rooms Rooms { get; } = new Rooms();


        public static List<string> loadedFiles { get; } = new List<string>();
        
        public static string notification {get; set;} = "";
    }

}