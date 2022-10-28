using Rubicon.Wow.CleanCode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubicon.Wow.CleanCode.Example;
public class DisneyCharacterService
{
    private HttpClient client = new HttpClient();
    private List<DisneyCharacter> cumulatedCharacters = new();
    private DisneyServiceRequestPage requestPage = new();

    public async Task FetchCharacters()
    {
        // Retrieve all disney characters
        do
        {

            Console.WriteLine($"Retrieving page {requestPage.Page}");
            var httpResponse = await client.GetAsync($"https://api.disneyapi.dev/characters?page={requestPage.Page}");

            if (httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    try
                    {
                        // Door serialize naar eigen class te verplaatsen is hier minder verantwoordelijkheid 
                        var characters = await JsonSerilization.DeserializeAsync<DisneyCharacters>(contentStream);
                        cumulatedCharacters.AddRange(characters.Data);
                        requestPage.TotalPages = characters.TotalPages;
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

        } while (requestPage.Page++ <= requestPage.TotalPages);
    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostMovieAppeances(int count) {
        // find top 5 disney characters with most movie appearances
        var t5cma = cumulatedCharacters.OrderByDescending(x => x.Films.Count).Take(count);
        int i = 1;

        foreach (var item in t5cma)
        {
            Console.WriteLine($"{i}. {item.Name} ({item.Films.Count})");
            i++;
        }

        return t5cma;

    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostVideoGameAppeances(int count)
    {
        // find top 5 disney characters with most video game appearances
        var t5cga = cumulatedCharacters.OrderByDescending(x => x.videoGames.Count).Take(count);
        int i = 1;

        foreach (var item in t5cga)
        {
            Console.WriteLine($"{i}. {item.Name} ({item.videoGames.Count})");
            i++;
        }

        return t5cga;
    }

    public IEnumerable<string>? GetMostFavoriteAllies(int count)
    {

        // create a superhero squad of most favored allies
        var mostFavoredAllies = cumulatedCharacters
            .SelectMany(x => x.Allies)
            .GroupBy(x => x)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Select(x => x.Name)
            .Take(count);

        if (mostFavoredAllies != null)
        {
            foreach (var item in mostFavoredAllies)
            {
                Console.WriteLine($"{item}");
            }
        }

        return mostFavoredAllies;
    }

}