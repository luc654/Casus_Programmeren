using System;
using System.IO;

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
        
        
        terminalHelper helper = new terminalHelper();
        
        int selectedFile = helper.handleTerminal(files, "Data inladen", "Selecteer een bestand");
        string selectedPath = files[selectedFile];
        Program.GlobalContext.loadedFiles.Add(selectedPath);
        
        return;   
    }

}