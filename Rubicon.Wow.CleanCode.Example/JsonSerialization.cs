public class JsonSerialization
{
    private static JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };

    public static async Task<T?> DeserializeAsync<T>(Stream data, JsonSerializerOptions? options = null)
    {
        return await JsonSerializer.DeserializeAsync<T>(
            data,
            _options);
    }
}
