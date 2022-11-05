using System.Text.Json.Serialization;

namespace Rubicon.Wow.CleanCode.Data;
public record DisneyCharacter(
    string[] Films,
    string[] ShortFilms,
    string[] TvShows,
    string[] VideoGames,
    string[] ParkAttractions,
    string[] Allies,
    string[] Enemies,
    string Name,
    string ImageUrl,
    string Url
)
{
    [JsonPropertyName("_id")]
    public int Id { get; init; }
}

