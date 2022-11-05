namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public class JsonSerialization
{
    private static JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };

    public static async Task<T?> DeserializeAsync<T>(Stream data)
    {
        return await JsonSerializer.DeserializeAsync<T>(
            data,
            _options);
    }

    public static async Task<string> SerializeAsync<T>(T data) 
    {
        MemoryStream stream = new MemoryStream();
        await JsonSerializer.SerializeAsync<T>(
            stream,
            data,
            _options);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}
