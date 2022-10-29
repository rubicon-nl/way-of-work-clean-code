using System.Text.Json.Serialization;

namespace Rubicon.Wow.CleanCode.Data;
public record DisneyCharacter(
    string Name,
    string ImageUrl,
    string Url    
) : DisneyCharacterbase(Name, ImageUrl, Url)
{
    public EquatableHashSet<string> Films { get; init; } = new EquatableHashSet<string>();
    public EquatableHashSet<string> ShortFilms { get; init; } = new EquatableHashSet<string>();
    public EquatableHashSet<string> TvShows { get; init; } = new EquatableHashSet<string>();
    public EquatableHashSet<string> VideoGames { get; init; } = new EquatableHashSet<string>();
    public EquatableHashSet<string> ParkAttractions { get; init; } = new EquatableHashSet<string>();
    public EquatableHashSet<string> Allies { get; init; } = new EquatableHashSet<string>();
    public EquatableHashSet<string> Enemies { get; init; } = new EquatableHashSet<string>();
}


public record DisneyCharacterbase(
    string Name,
    string ImageUrl,
    string Url
)
{
    [JsonPropertyName("_id")]
    public int Id { get; init; }

}

