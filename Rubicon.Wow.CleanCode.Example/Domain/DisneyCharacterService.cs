namespace Rubicon.Wow.CleanCode.Example.Domain;

public class DisneyCharacterService
{
    public async Task TopMovieAppearances(List<DisneyCharacter> cumulatedCharacters, int amount)
    {
        // find top 5 disney characters with most movie appearances
        var t5cma = cumulatedCharacters.OrderByDescending(x => x.films.Count).Take(amount);
        int i = 1;

        foreach (var item in t5cma)
        {
            Console.WriteLine($"{i}. {item.name} ({item.films.Count})");
            i++;
        }
    }

    public async Task TopGameAppearances(List<DisneyCharacter> cumulatedCharacters, int amount)
    {
        // find top 5 disney characters with most movie appearances
        var t5cga = cumulatedCharacters.OrderByDescending(x => x.videoGames.Count).Take(amount);
        int i = 1;

        foreach (var item in t5cga)
        {
            Console.WriteLine($"{i}. {item.name} ({item.videoGames.Count})");
            i++;
        }


    }

    public async Task CreateSuperHeroSquad(List<DisneyCharacter> cumulatedCharacters, int amount)
    {
        // create a superhero squad of most favored allies
        var mostFavoredAllies = cumulatedCharacters
            .SelectMany(x => x.allies)
            .GroupBy(x => x)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Select(x => x.Name)
            .Take(amount);

        if (mostFavoredAllies != null)
        {
            foreach (var item in mostFavoredAllies)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
