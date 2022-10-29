using Rubicon.Wow.CleanCode.Data;

namespace Rubicon.Wow.CleanCode.Example;
public interface IDisneyCharacterService
{
    Task<bool> FetchCharactersAsync();
    IEnumerable<string>? GetMostFavoriteAllies(int count);
    IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostMovieAppeances(int count);
    IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostVideoGameAppeances(int count);
}