using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace MadHub;

public class EditorManifestData
{
    public string Name { get; set; } = "MadEditor";
    public string Path { get; set; } = string.Empty;
    public string PathToExecutable { get; set; } = string.Empty;
    public Version Version { get; set; } = new Version(1, 0, 0);
    public DateTime ReleaseDate { get; set; } = new(2026, 9, 27);
    public bool IsLts { get; set; }
    public List<string> Platforms { get; set; } = [];
}

public static class EditorManifestDataManager
{
    public static ObservableCollection<EditorManifestData> Datas = [];
    private static readonly string FileName = "editors.txt";
    private static string FilePath => Path.Combine(ApplicationData.PersistentDataPath, FileName);

    public static void Add(string path)
    {
        path = Path.Combine(path, "manifest.json");
        
        LoadEditorData(path);
        Save();
    }

    public static void Remove(EditorManifestData data)
    {
        Datas.Remove(data);
        Save();
    }

    public static EditorManifestData? GetEditor(Version version)
    {
        return Datas.FirstOrDefault(x => x.Version == version);
    }
    
    public static void Load()
    {
        Datas.Clear();
        if (!File.Exists(FilePath))
            return;
        
        string data = File.ReadAllText(FilePath);
        string[] lines = data.Split("\n");
        foreach (string path in lines)
        {
            LoadEditorData(path);
        }
    }

    public static void LoadEditorData(string path)
    {
        if (!File.Exists(path)) return;

        string data = File.ReadAllText(path);
        
        EditorManifestData? manifest = JsonSerializer.Deserialize<EditorManifestData>(data, SerializerSettings.SerializerOptions);

        if (manifest == null) return;
        manifest.Path = path;
        
        Datas.Add(manifest);
    }

    public static void Save()
    {
        if(!File.Exists(FilePath))
            File.Create(FilePath).Dispose();
        
        File.WriteAllText(FilePath, string.Join("\n", Datas.Select(x => x.Path)));
    }
}