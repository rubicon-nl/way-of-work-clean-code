namespace Rubicon.Wow.CleanCode.Data;

public record DisneyCharacters
{
    public DisneyCharacter[]? Data { get; init; }
    public int Count { get; init; }
    public int TotalPages { get; init; }
    public string? NextPage { get; init; }
}
