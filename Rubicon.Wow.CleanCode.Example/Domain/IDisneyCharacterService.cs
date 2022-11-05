using Rubicon.Wow.CleanCode.Data;

namespace Rubicon.Wow.CleanCode.Example.Domain;

public interface IDisneyCharacterService
{
    IEnumerable<string>? GetMostFavoriteAllies(int count);
    IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostMovieAppeances(int count);
    IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostVideoGameAppeances(int count);
}