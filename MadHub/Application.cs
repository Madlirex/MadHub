using System.IO;

namespace MadHub;

public static class ApplicationData
{
    private static string PersistentDataPathFolder => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public static string PersistentDataPath
    {
        get
        {
            string path = PersistentDataPathFolder;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}