using System;
using System.Collections.Generic;

namespace Casus_Programmeren;

public class terminalHelper
{
    private ConsoleColor selectedOption = ConsoleColor.Black;
    private ConsoleColor selectedBackground = ConsoleColor.White;
    
    private ConsoleColor defaultOption = ConsoleColor.White;
    private ConsoleColor defaultBackground = ConsoleColor.Black;
    
    private ConsoleColor headerColor = ConsoleColor.Green;


    private void showOptions(List<string> options, int selectedIndex)
    {

        int index = -1;
        foreach (string entry in options)
        {
            index++;

            if (index == selectedIndex)
            {
                Console.BackgroundColor = selectedBackground;
                Console.ForegroundColor = selectedOption;
            }
            else
            {
                Console.BackgroundColor = defaultBackground;
                Console.ForegroundColor = defaultOption;
            }
            
        Console.WriteLine($"[{index + 1}] {entry}");
        Console.ResetColor();
        }
    }

    private void clearDisplay()
    {
        Console.Clear();
    }

    /// <summary>
    /// Takes a list<string> of options, returns the selected option index (int)
    /// </summary>
    public int handleTerminal(List<string> options, string title)
    {
        int selectedIndex = 0;
        bool selected = false;
        int listLength = options.Count;
        
    
        while (!selected)
        {

            clearDisplay();
            
            displayTitle(title);
        
            showOptions(options, selectedIndex);
            ConsoleKeyInfo key = Console.ReadKey(true);

            Console.WriteLine(key.KeyChar);
            if (key.Key == ConsoleKey.Enter)
            {
                selected = true;
            } else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedIndex++;

                selectedIndex = selectedIndex > listLength - 1 ? 0 : selectedIndex;
            } else if (key.Key == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                
                selectedIndex = selectedIndex < 0 ? listLength - 1 : selectedIndex;
                
                // c# wizardry, if statement = true selectedIndex becomes the output of int.tryparse (oftewel becomes [pressed key])
            } else if (int.TryParse(key.KeyChar.ToString(), out selectedIndex))
            {
                // Since showOptions uses 1-based indexing in the visual side we need to subtract 1 from the selectedIndex
                selectedIndex--;
                selectedIndex = selectedIndex > listLength - 1 ? listLength -1 : selectedIndex;
                selectedIndex = selectedIndex < 1 ? 0 : selectedIndex;
            }
        }
        
        return selectedIndex;
    }

    private void displayTitle(string title)
    {

        Console.ForegroundColor = headerColor;
        
        Console.WriteLine("=================");
        Console.WriteLine(title);
        Console.WriteLine("=================");
        
        Console.WriteLine();
        
        Console.ResetColor();

    }
}