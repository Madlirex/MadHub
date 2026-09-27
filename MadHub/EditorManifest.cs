using System.Collections.ObjectModel;
using System.Text.Json;

namespace MadHub;

public class EditorManifestData
{
    public readonly string Name = "MadEditor";
    public readonly Version Version = new Version(1, 0, 0);
    public readonly DateTime ReleaseDate = new(2026, 9, 27);
    public bool IsLts { get; set; }
    public List<string> Platforms { get; set; } = new List<string>();
}

public static class EditorManifestDataManager
{
    public static ObservableCollection<EditorManifestData> Datas = [];
    
    public static void Load()
    {
        
    }
}