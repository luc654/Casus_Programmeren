using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Casus_Programmeren
{
    public class JsonLoader
    {
        public GraphData loadJson()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string projectRoot = Path.Combine(currentDir, "..", "..", "..");
            string dataFolder = Path.Combine(projectRoot, "data");
            string jsonFilePath = Path.Combine(dataFolder, "edges.json");

            if (!File.Exists(jsonFilePath))
            {
                Program.GlobalContext.notification = "Fout bij inladen data, file niet gevonden!";
                throw new FileNotFoundException($"JSON file not found: {jsonFilePath}");
                
            }

            string jsonContent = File.ReadAllText(jsonFilePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var graphData = JsonSerializer.Deserialize<GraphData>(jsonContent, options);

            if (graphData == null)
            {
                Program.GlobalContext.notification = "Fout bij inladen data!";
                throw new Exception("Graph data failed to serialize");
            }
            
          
            return graphData;
        }
    }
}
