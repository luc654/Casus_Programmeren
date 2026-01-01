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
            "Als beheerder verder gaan",
            "Als student verder gaan",
            "Als Docent verder gaan",
            "Reserveringen inladen",
            "Kamers inladen",
        };

        terminalHelper helper = new terminalHelper();
     

        
        
         


        switch (helper.handleTerminal(options, "Casus Programmeren", "Selecteer een optie om de Casus te beginnen"))
        {
            case 0:
            {
                GlobalContext.CurrentUserRole = UserRole.Admin;
                AdminHub.loop();
                break;
            }

            case 1:
            {
                GlobalContext.CurrentUserRole = UserRole.Student;
                UserHub.loop();
                break;
            }

            case 2:
            {
                GlobalContext.CurrentUserRole = UserRole.Docent;
                DocentHub.loop();
                break;
            }
            
            case 3:
            {
                DataLoader dataLoader = new DataLoader();
                dataLoader.Load();
                break;
            }

            case 4:
            {
                RoomsLoader roomsLoader = new RoomsLoader();
                roomsLoader.loadRooms();
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
        Admin,
        Docent
    }

    /// <summary>
    /// Global variables holder.
    /// </summary>
    public static class GlobalContext
    {
        public static UserRole CurrentUserRole { get; set; }
        public static Rooms Rooms { get; } = new Rooms();
        public static Reservations  Reservations { get; } = new Reservations();

        public static List<string> loadedFiles { get; } = new List<string>();
        
        public static string notification {get; set;} = "";
    }

}