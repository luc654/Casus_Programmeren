using System;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace Casus_Programmeren;

public class DataLoader
{
    
    public void Load()
    {

        // Because of C# idiocracy we cant just access the root directory of the project, nooo we have to use a frail cluster fuck of code that
        // moves a few directories upwards to access the root.
        string currentDir = Directory.GetCurrentDirectory();
        string projectRoot = Path.Combine(currentDir, "..", "..", "..");
        string dataFolder = Path.Combine(projectRoot, "data");

        
        
        
        string[] filesArr = Directory.GetFiles(dataFolder);
        List<string> files = new List<string>(filesArr);
        
        // Remove previously loaded files from list.
        files.RemoveAll(file => Program.GlobalContext.loadedFiles.Contains(file));

        // Remove all non ics files
        files.RemoveAll(file => file.EndsWith("json"));
        
        if (files.Count == 0)
        {
            Program.GlobalContext.notification = "Geen files te importeren!";
            return;

        }
        
        terminalHelper helper = new terminalHelper();
        
        int selectedFile = helper.handleTerminal(files, "Data inladen", "Selecteer een bestand");
        string selectedPath = files[selectedFile];
        Program.GlobalContext.loadedFiles.Add(selectedPath);
        
        string content = readFile(selectedPath);

        int amount = generateEntries(content);

        Program.GlobalContext.notification = $"{amount} of reservations imported!";

    }


    private string readFile(string path)
    {
     string contents = File.ReadAllText(path); 
     
     return contents;
    }
    
    
    // Bear with me, an ics file contains data spread with linebreaks, an entry begins with BEGIN which is nice. So in a sense an ics file is a nested array which can be parsed with two explodes. So parsing it should not be hard nor even require regex (no shame to the vibe coders who used regex n stuff)

    private int generateEntries(string data)
{
    // Split the raw data into entries
    string[] entries = data.Split("BEGIN", StringSplitOptions.RemoveEmptyEntries);        

    foreach (var entry in entries)
    {
        if (string.IsNullOrWhiteSpace(entry))
            continue;

        string[] lines = entry.Split(
            new string[] { Environment.NewLine },
            StringSplitOptions.None
        );

        if (lines.Length < 9)
            continue;

        string uid = "";
        string summary = "";
        string location = "";
        string building = "";
        Building rBuilding = Building.Spectrum;
        var attendees = new List<string>();
        DateTimeOffset start = default;
        DateTimeOffset end = default;

        // Not efficient, but works. Checks EACH line of the entry and determines what kind of value it is based on the starting value, e.g. UID: is the Unique Identifier.
        foreach (var line in lines)
        {
            if (line.StartsWith("UID:"))
            {
                uid = line.Split(':')[1];
            }
            else if (line.StartsWith("SUMMARY:"))
            {
                summary = line.Split(':', 2)[1].Replace(@"\,", ",");
            }
            else if (line.StartsWith("LOCATION:"))
            {
                string buildinglocation = line.Split(':')[1];
                building =  buildinglocation.Split(' ')[0];
                
                int index = buildinglocation.IndexOf(' ');
                location = buildinglocation.Substring(index + 1);
            }
            else if (line.StartsWith("DTSTART"))
            {
                string datePart = line.Split(':')[1];
                start = DateTimeOffset.ParseExact(datePart, "yyyyMMdd'T'HHmmss", CultureInfo.InvariantCulture);
            }
            else if (line.StartsWith("DTEND"))
            {
                string datePart = line.Split(':')[1];
                end = DateTimeOffset.ParseExact(datePart, "yyyyMMdd'T'HHmmss", CultureInfo.InvariantCulture);
            }
            else if (line.StartsWith("ATTENDEE"))
            {
                string email = line.Split(':')[1].Replace("MAILTO:", "", StringComparison.OrdinalIgnoreCase);
                attendees.Add(email);
            }
        }

        if (building == "P")
        {
            rBuilding = Building.Prisma;
        } else if (building == "S")
        {
            rBuilding = Building.Spectrum;
        }

        Reservation reservation = new Reservation(
            uid,
            start,
            end,
            summary,
            attendees,
            location,
            rBuilding
        );
        
        Program.GlobalContext.Reservations.addReservation(reservation);
        
    }

    return entries.Length;
}

    
}
