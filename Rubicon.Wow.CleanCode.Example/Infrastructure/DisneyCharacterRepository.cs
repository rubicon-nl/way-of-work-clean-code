using Rubicon.Wow.CleanCode.Data;
using System.Collections.ObjectModel;

namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public class DisneyCharacterRepository : IDisneyCharacterRepository
{
    private readonly HttpClient _httpClient;
    private List<DisneyCharacter> _cumulatedCharacters = new();
    private DisneyServiceRequestPage _requestPage = new();
    private readonly IOutputWriter _outputWriter;

    public ReadOnlyCollection<DisneyCharacter> DisneyCharacters => new ReadOnlyCollection<DisneyCharacter>(_cumulatedCharacters);

    public DisneyCharacterRepository(IHttpClientDecorator httpClientDecorator, IOutputWriter outputWriter)
    {
        _httpClient = httpClientDecorator.Create("Disney");
        _outputWriter = outputWriter;
    }

    public async Task<DisneyCharacters> GetAsync()
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
                        _cumulatedCharacters.AddRange(characters!.Data.ToList());
                        _requestPage.TotalPages = characters.TotalPages;
                    }
                    catch (JsonException)
                    {
                        _outputWriter.WriteLine("Invalid JSON.");
                        return null!;
                    }
                }
                else
                {
                    _outputWriter.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                    return null!;
                }
            }
            else
            {
                _outputWriter.WriteLine("HTTP Response error");
                return null!;
            }

        } while (_requestPage.Page++ <= _requestPage.TotalPages);
        return null!;
    }
}


