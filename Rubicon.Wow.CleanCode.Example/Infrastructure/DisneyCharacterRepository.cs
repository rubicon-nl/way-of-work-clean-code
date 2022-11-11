using Rubicon.Wow.CleanCode.Example.Domain;

namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public class DisneyCharacterRepository : IDisneyCharacterRepository
{
    public async Task<List<DisneyCharacter>> GetDisneyCharacters()
    {
        var page = 1;
        var totalPages = 1;

        HttpClient client = new HttpClient();
        var cumulatedCharacters = new List<DisneyCharacter>();

        // Retrieve all disney characters
        do
        {
            Console.WriteLine($"Retrieving page {page}");
            var httpResponse = await client.GetAsync($"https://api.disneyapi.dev/characters?page={page}");

            if (httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    try
                    {
                        var characters = await JsonSerializer.DeserializeAsync<DisneyCharacters>(contentStream);
                        cumulatedCharacters.AddRange(characters.data);
                        totalPages = characters.totalPages;
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine("Invalid JSON.");
                    }
                }
                else
                {
                    Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                }
            }
            else
            {
                Console.WriteLine("HTTP Response error");
            }

            page++;
        } while (page <= totalPages);

        return cumulatedCharacters;
    }

}
