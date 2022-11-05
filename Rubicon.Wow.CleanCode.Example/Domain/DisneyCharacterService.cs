using Rubicon.Wow.CleanCode.Data;
using System.Collections.ObjectModel;

namespace Rubicon.Wow.CleanCode.Example.Domain;

public class DisneyCharacterService : IDisneyCharacterService
{        
    public ReadOnlyCollection<DisneyCharacter> DisneyCharacters { get; init; }

    public DisneyCharacterService(IEnumerable<DisneyCharacter> disneyCharacters)
    {
        DisneyCharacters = new ReadOnlyCollection<DisneyCharacter>(disneyCharacters.ToList());
    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostMovieAppeances(int count)
    {
        // find top 5 disney characters with most movie appearances
        var t5cma = DisneyCharacters.OrderByDescending(x => x.Films.Count).Take(count);
        int i = 1;

        return t5cma;

    }

    public IEnumerable<DisneyCharacter> GetTopDisneyCharactersWithMostVideoGameAppeances(int count)
    {
        // find top 5 disney characters with most video game appearances
        var t5cga = DisneyCharacters.OrderByDescending(x => x.VideoGames.Count).Take(count);
        int i = 1;

        
        return t5cga;
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

        if (mostFavoredAllies != null)
        {
          
        }

        return mostFavoredAllies;
    }

}