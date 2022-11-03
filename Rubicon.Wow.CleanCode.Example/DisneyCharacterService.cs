using Rubicon.Wow.CleanCode.Data;
using System.Collections.ObjectModel;

namespace Rubicon.Wow.CleanCode.Example;
public class DisneyCharacterService : IDisneyCharacterService
{
    private readonly HttpClient _httpClient;
    private List<DisneyCharacter> _cumulatedCharacters = new();
    private DisneyServiceRequestPage _requestPage = new();
    private readonly IOutputWriter _outputWriter;

    public ReadOnlyCollection<DisneyCharacter> DisneyCharacters => new ReadOnlyCollection<DisneyCharacter>(_cumulatedCharacters);

    public DisneyCharacterService(IHttpClientDecorator httpClientDecorator, IOutputWriter outputWriter)
    {
        _httpClient = httpClientDecorator.Create("Disney");
        _outputWriter = outputWriter;
    }
        
    public void SetCharacterList(IEnumerable<DisneyCharacter> characters)
    {
        _cumulatedCharacters.Clear();
        _cumulatedCharacters.AddRange(characters);
    }

    public async Task<bool> FetchCharactersAsync()
    {
        // Retrieve all disney characters
        do
        {
            _outputWriter.WriteLine($"Retrieving page {_requestPage.Page}");
            var httpResponse = await _httpClient.GetAsync($"characters?page={_requestPage.Page}");

            if (httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    try
                    {
                        // Door serialize naar eigen class te verplaatsen is hier minder verantwoordelijkheid 
                        var characters = await JsonSerialization.DeserializeAsync<DisneyCharacters>(contentStream);

                        _cumulatedCharacters.AddRange(characters.Data!);
                        _requestPage.TotalPages = characters.TotalPages;
                    }
                    catch (JsonException)
                    {
                        _outputWriter.WriteLine("Invalid JSON.");
                        return false;
                    }
                }
                else
                {
                    _outputWriter.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                    return false;
                }
            }
            else
            {
                _outputWriter.WriteLine("HTTP Response error");
                return false;
            }

        } while (_requestPage.Page++ < _requestPage.TotalPages);
        return true;
    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostMovieAppeances(int count)
    {
        // find top 5 disney characters with most movie appearances
        var t5cma = _cumulatedCharacters.OrderByDescending(x => x.Films.Count).Take(count);
        int i = 1;

        foreach (var item in t5cma)
        {
            _outputWriter.WriteLine($"{i}. {item.Name} ({item.Films.Count})");
            i++;
        }

        return t5cma;

    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostVideoGameAppeances(int count)
    {
        // find top 5 disney characters with most video game appearances
        var t5cga = _cumulatedCharacters.OrderByDescending(x => x.VideoGames.Count).Take(count);
        int i = 1;

        foreach (var item in t5cga)
        {
            _outputWriter.WriteLine($"{i}. {item.Name} ({item.VideoGames.Count})");
            i++;
        }

        return t5cga;
    }

    public IEnumerable<string>? GetMostFavoriteAllies(int count)
    {

        // create a superhero squad of most favored allies
        var mostFavoredAllies = _cumulatedCharacters
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
                _outputWriter.WriteLine($"{item}");
            }
        }

        return mostFavoredAllies;
    }

}