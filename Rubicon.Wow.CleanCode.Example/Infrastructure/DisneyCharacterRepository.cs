using Rubicon.Wow.CleanCode.Example.Domain;

namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public class DisneyCharacterRepository : IDisneyCharacterRepository
{
    private readonly HttpClient client;
    private readonly ILogger<DisneyCharacterRepository> logger;

    public DisneyCharacterRepository(ILogger<DisneyCharacterRepository> logger)
    {
        client = new HttpClient();
        this.logger = logger;
    }

    public async Task<List<DisneyCharacter>> GetDisneyCharacters()
    {
        int page = 1;
        int totalPages;

        var cumulatedCharacters = new List<DisneyCharacter>();

        do
        {
            DisneyCharacters characters = await RetrievePage(page);

            cumulatedCharacters.AddRange(characters.data);
            totalPages = characters.totalPages;

            page++;
        } while (page <= totalPages);

        return cumulatedCharacters;
    }

    private async Task<DisneyCharacters> RetrievePage(int page)
    {
        logger.LogTrace($"Retrieving page {page}");
        var httpResponse = await client.GetAsync($"https://api.disneyapi.dev/characters?page={page}");

        httpResponse.EnsureSuccessStatusCode();

        if (httpResponse.Content is null || httpResponse.Content.Headers.ContentType.MediaType != "application/json")
        {
            throw new Exception("HTTP Response was invalid and cannot be deserialised.");
        }

        var contentStream = await httpResponse.Content.ReadAsStreamAsync();

        var characters = await JsonSerializer.DeserializeAsync<DisneyCharacters>(contentStream);

        ArgumentNullException.ThrowIfNull(characters);
        return characters;
    }
}
