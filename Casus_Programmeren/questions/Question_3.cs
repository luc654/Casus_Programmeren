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
            Console.WriteLine(room.roomNumber);
            roomIds.Add(room.roomNumber);
        }

        Console.ReadLine();
        int startRoomId = helper.handleTerminal(roomIds, "Route berekenen", "Selecteer uw startpunt");

        var nData = data.Nodes.FirstOrDefault(n => n.Id == startRoomId);
        var jsonid = nodeMap.FirstOrDefault(x => x.Value == nData.Id).Key;
        
        Console.WriteLine(jsonid);
        Console.ReadLine();
        
        var startNode = nodeMap[1];  // JSON ID 1
        var endNode = nodeMap[30];   // JSON ID 30
        
        ShortestPathResult result = graph.Dijkstra(startNode, endNode);

        // 4. Check if path exists and log
        if (result.IsFounded)
        {
            var path = result.GetPath();
            Console.WriteLine($"Path found with distance: {result.Distance}");
            
            foreach (var internalId in path)
            {
                // Find the node title in your original data using the mapping
                var jsonId = nodeMap.FirstOrDefault(x => x.Value == internalId).Key;
                var nodeData = data.Nodes.FirstOrDefault(n => n.Id == jsonId);
                Console.WriteLine($"Step: {nodeData?.Title} (ID: {jsonId})");
            }
        }
        else
        {
            Console.WriteLine("No path found between the specified nodes.");
        }

        Console.ReadLine();
    }
    
}