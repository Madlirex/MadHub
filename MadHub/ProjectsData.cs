using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace MadHub;

public class ProjectInfo
{
    public string Name { get; set; } = "New Project";
    public string Description { get; set; } = "Cool new game!";
    public Version Version { get; set; } = new(1, 0, 0);
    public Version EditorVersion { get; set; } = new(0, 0, 0);
    public DateTime LastOpened { get; set; } = DateTime.MinValue;
    
    public string Author { get; set; } = "DefaultAuthor";
    public string Company { get; set; } = "DefaultCompany";
    public string Path { get; set; } = string.Empty;
    
    public string ModifiedDisplay => GetRelativeTime(LastOpened);

    private static string GetRelativeTime(DateTime dateTime)
    {
        TimeSpan timeSpan = DateTime.Now - dateTime;

        if (timeSpan.TotalSeconds < 60) 
            return "Just now";
        
        if (timeSpan.TotalMinutes < 60) 
            return $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes != 1 ? "s" : "")} ago";
        
        if (timeSpan.TotalHours < 24) 
            return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours != 1 ? "s" : "")} ago";
        
        if (timeSpan.TotalDays < 30) 
            return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays != 1 ? "s" : "")} ago";
        
        if (timeSpan.TotalDays < 365) 
        {
            int months = (int)(timeSpan.TotalDays / 30);
            return $"{months} month{(months != 1 ? "s" : "")} ago";
        }
    
        int years = (int)(timeSpan.TotalDays / 365);
        return $"{years} year{(years != 1 ? "s" : "")} ago";
    }

}

public static class ProjectInfoManager
{
    public static ObservableCollection<ProjectInfo> Infos = [];

    private static string FileName => "projects.txt";
    private static string FilePath => Path.Combine(ApplicationData.PersistentDataPath, FileName);

    public static void Add(string path)
    {
        path = Path.Combine(path, "project.madx");
        
        LoadProjectInfo(path);
        Save();
    }

    public static void Remove(ProjectInfo info)
    {
        Infos.Remove(info);
    }
    
    public static void Load()
    {
        if (!File.Exists(FilePath)) return;

        string[] paths = File.ReadAllText(FilePath).Split("\n");

        foreach (string path in paths)
        {
            LoadProjectInfo(path);
        }
    }

    public static void OpenProject(ProjectInfo project)
    {
        if (EditorManifestDataManager.GetEditor(project.EditorVersion) is { } editor)
        {
            string? executableDirectory = Path.GetDirectoryName(editor.PathToExecutable);
            string? projectDirectory = Path.GetDirectoryName(project.Path);
            Console.WriteLine(executableDirectory);
            Console.WriteLine(projectDirectory);
            
            if (string.IsNullOrEmpty(executableDirectory) || string.IsNullOrEmpty(projectDirectory))
            {
                throw new InvalidOperationException("Could not resolve executable or project directories.");
            }
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = editor.PathToExecutable,
                WorkingDirectory = executableDirectory, 
                Arguments = projectDirectory
            };

            Process.Start(startInfo);
        }
    }

    public static void LoadProjectInfo(string path)
    {
        if (!File.Exists(path)) return;

        string data = File.ReadAllText(path);
        ProjectInfo? info = JsonSerializer.Deserialize<ProjectInfo>(data);
        if (info == null) return;

        info.Path = path;
        Infos.Add(info);
    }

    public static void Save()
    {
        if(!File.Exists(FilePath))
            File.Create(FilePath).Dispose();
        
        File.WriteAllText(FilePath, string.Join("\n", Infos.Select(x => x.Path)));
    }
}