using System;
using Casus_Programmeren;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        List<string> options = new List<string>
        {
            "-h|--help",
            "-v|--version",
            "-f|--force",
            "-o|--output",
            "-a|--append",
        };

     
        terminalHelper helper = new terminalHelper();

        helper.handleTerminal(options);
    }
}