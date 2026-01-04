using System;
using System.Collections.Generic;
using System.Linq;
using Casus_Programmeren;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

public class Question_3
{
    public static void question3()
    {
        // This code is complex enough as is. Due to the complexity of using external libraries and algorithms the decision has been made to not abstract this class any further. Instead, this function has been chaptered
        
        // ----------------
        // Initialize Graph / map
        // ----------------
        
        var graph = new Graph<string, string>();
        var nodeMap = new Dictionary<int, uint>(); 
        
        JsonLoader loader = new JsonLoader();
        GraphData data = loader.loadJson();

        foreach (var node in data.Nodes)
        {
            uint internalId = graph.AddNode(node.Title);
            nodeMap.Add(node.Id, internalId);
        }

        // Make connections.
        foreach (var edge in data.Edges)
        {
            graph.Connect(nodeMap[edge.From], nodeMap[edge.To], edge.Distance, "");
        }

        
                
        // ----------------
        // Request information from user
        // ----------------

        List<string> buildingQuestions = new List<string>()
        {
            "Prisma",
            "Spectrum"
        };

        
        terminalHelper helper = new terminalHelper(); 
        int decision = helper.handleTerminal(buildingQuestions, "Route berekenen", "Selecteer gebouw van uw startpunt");
        
        List<Room> rooms = new List<Room>();
        if (decision == 0)
        {
            rooms = Program.GlobalContext.Rooms.getRooms()
                .Where(room => room.building == Building.Prisma)
                .ToList();
            
        }
        else if (decision == 1)
        {
            rooms = Program.GlobalContext.Rooms.getRooms()
                .Where(room => room.building == Building.Spectrum)
                .ToList();
        }

        List<String> roomIds = new List<string>();
        
        
        foreach (var room in rooms)
        {
            roomIds.Add(room.roomNumber);
        }

        string startRoomTitle;
        string endRoomTitle;
        do
        {
            startRoomTitle = helper.handleQuestion("Voer Start ruimte nummer in.");
        } while (validateInput(startRoomTitle));
        
        
        do
        {
            endRoomTitle = helper.handleQuestion("Voer Bestemming Ruimte nummer in.");
        } while (validateInput(endRoomTitle));
        
        // ------------
        //  Calculate shortest path using Dijkstra.net
        // ------------
        
        // Get node ID using title.
        var nData = data.Nodes.FirstOrDefault(n => n.Title == startRoomTitle);
        var eData = data.Nodes.FirstOrDefault(n => n.Title == endRoomTitle);

        
        // If one data exists in rooms.json but not in edges.json, or otherwise, this gets thrown
        if (nData == null || eData == null) 
        {
            Program.GlobalContext.notification = "error 500, gebruik google maps...";
            return;
        }
        
        // Get nodemap nodeID using the dictionary
        uint startNodeId = nodeMap[nData.Id];
        uint endNodeId = nodeMap[eData.Id];
        
        ShortestPathResult result = graph.Dijkstra(startNodeId, endNodeId);

        if (result.IsFounded)
        {
            var path = result.GetPath();
            var index = 0;
            foreach (var internalId in path)
            {
                index++;
                var jsonId = nodeMap.FirstOrDefault(x => x.Value == internalId).Key;
                var nodeData = data.Nodes.FirstOrDefault(n => n.Id == jsonId);
                Console.WriteLine($"stap {index}: Loop richting {nodeData?.Title}");
            } 
            Console.WriteLine("Bestemming bevind zich naast u.");
        }
        else
        {
            Program.GlobalContext.notification = "Geen route gevonden...";
            return;
        }

        Console.ReadLine();
    }

    private static bool validateInput(string input)
    {
        int index = Program.GlobalContext.Rooms.getRooms().FindIndex(room => room.roomNumber == input);

        if (index == -1)
        {
            Program.GlobalContext.notification = $"Geen kamer gevonden met nummer {input}";
            return true;
        }
        else
        {
            return false;
        }
    }
}