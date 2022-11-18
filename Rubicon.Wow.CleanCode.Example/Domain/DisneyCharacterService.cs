using Rubicon.Wow.CleanCode.Data;
using System.Collections.ObjectModel;

namespace Rubicon.Wow.CleanCode.Example.Domain;

public class DisneyCharacterService : IDisneyCharacterService
{        
    public IQueryable<DisneyCharacter> DisneyCharacters { get; init; }

    public DisneyCharacterService(IQueryable<DisneyCharacter> disneyCharacters)
    {
        DisneyCharacters = disneyCharacters;
    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostMovieAppeances(int count)
    {
        // find top 5 disney characters with most movie appearances
        return DisneyCharacters.OrderByDescending(x => x.Films.Count).Take(count);
        
    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostVideoGameAppeances(int count)
    {
        // find top 5 disney characters with most video game appearances
        return DisneyCharacters.OrderByDescending(x => x.VideoGames.Count).Take(count);        
    }

    public IEnumerable<string>? GetMostFavoriteAllies(int count)
    {

        // create a superhero squad of most favored allies
        var mostFavoredAllies = DisneyCharacters
            .SelectMany(x => x.Allies)
            .GroupBy(x => x)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Select(x => x.Name)
            .Take(count);

            return mostFavoredAllies;
    }

}