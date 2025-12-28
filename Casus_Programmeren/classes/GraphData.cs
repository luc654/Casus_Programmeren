namespace Casus_Programmeren;

public class GraphData
{
    public List<NodeDto> Nodes { get; set; }
    public List<EdgeDto> Edges { get; set; }
}

public class NodeDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class EdgeDto
{
    public int From { get; set; }
    public int To { get; set; }
    public int Distance { get; set; }
}
