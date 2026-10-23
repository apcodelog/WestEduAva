namespace Core.Models;

public record DataGroup
{
    public string Path { get; }
    public string TitleIndefinite { get; }
    public string TitleDefinite { get; }
    public string TitlePlural { get; }
    
    public DataGroup(string path, string titleIndefinite, string titleDefinite, string titlePlural)
    {
        Path = path;
        TitleIndefinite = titleIndefinite;
        TitleDefinite = titleDefinite;
        TitlePlural = titlePlural;
    }
}
