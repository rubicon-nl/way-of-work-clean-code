using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Rubicon.Wow.CleanCode.Data;

public record DisneyCharacters
{    
    public List<DisneyCharacter>? Data { get; init; }
    public int Count { get; init; }
    public int TotalPages { get; init; }
    public string? NextPage { get; init; } 
}
