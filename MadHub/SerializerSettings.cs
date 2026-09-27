using System.Text.Json;

namespace MadHub;

public static class SerializerSettings
{
    public static JsonSerializerOptions SerializerOptions => new JsonSerializerOptions()
    {
        WriteIndented = true,
        IndentSize = 4,
        IncludeFields = true
    };
}