using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Swift;

namespace Casus_Programmeren;

public class terminalHelper
{
    private ConsoleColor selectedOption = ConsoleColor.Black;
    private ConsoleColor selectedBackground = ConsoleColor.White;
    
    private ConsoleColor defaultOption = ConsoleColor.White;
    private ConsoleColor defaultBackground = ConsoleColor.Black;
    
    private ConsoleColor headerColor = ConsoleColor.Green;
    private ConsoleColor notificationColor = ConsoleColor.Yellow;
    
    private string title = "Casus Programmeren";
    private string description = "";
    private string notification = "";

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
    /// Takes a list of string of options, returns the selected option index (int)
    /// </summary>
    public int handleTerminal(List<string> options, string title="", string description="", string notification="")
    {
        int selectedIndex = 0;
        bool selected = false;
        int listLength = options.Count;
        setTitle(title);
        setDescription(description);
        
        // Handle eloquent notification model
        if (notification.Length > 0)
        {
        setNotification(notification);
        } else if (Program.GlobalContext.notification.Length > 0)
        {
            setNotification(Program.GlobalContext.notification);
            Program.GlobalContext.notification = "";
        }
    
        while (!selected)
        {

            clearDisplay();
            
            displayTitle();
            displayMeta();
            
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

    private void displayTitle()
    {

        if (title.Length > 0)
        {
            
        Console.ForegroundColor = headerColor;
        
        Console.WriteLine("=================");
        Console.WriteLine(title);
        Console.WriteLine("=================");
        
        Console.ResetColor();
        }

    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    private void displayMeta()
    {
        if (description.Length > 0)
        {
            Console.ForegroundColor = defaultOption;
            Console.BackgroundColor = defaultBackground;
            
            Console.Write(description);
            
            Console.ResetColor();
        }

        if (notification.Length > 0)
        {
            Console.ForegroundColor = notificationColor;
            Console.BackgroundColor = defaultBackground;
            Console.Write($"\t{notification}");
            Console.ResetColor();
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public void setDescription(string description)
    {
        this.description = description;
    }
    
    public void setNotification(string notification)
    {
        this.notification = notification;
    }
}