using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Rubicon.Wow.CleanCode.Data;
public record DisneyCharacter (
    List<string> Films,
    List<string> ShortFilms,
    List<string> TvShows,
    List<string> videoGames,
    List<string> ParkAttractions,
    List<string> Allies,
    List<string> Enemies,        
    string Name,
    string ImageUrl,
    string Url
)
{
    [JsonPropertyName("_id")]
    public int Id { get; init; }
}

