namespace Rubicon.Wow.CleanCode.Example.Domain;

public class DisneyCharacterService : IDisneyCharacterService
{
    private readonly List<DisneyCharacter> disneyCharacters;

    public DisneyCharacterService(IDisneyCharacterRepository disneyCharacterRepository)
    {
        this.disneyCharacters = disneyCharacterRepository.GetDisneyCharacters().Result;
    }

    public async Task TopMovieAppearances(int amount)
    {
        // find top 5 disney characters with most movie appearances
        var t5cma = disneyCharacters.OrderByDescending(x => x.films.Count).Take(amount);
        int i = 1;

        foreach (var item in t5cma)
        {
            Console.WriteLine($"{i}. {item.name} ({item.films.Count})");
            i++;
        }
    }

    public async Task TopGameAppearances(int amount)
    {
        // find top 5 disney characters with most movie appearances
        var t5cga = disneyCharacters.OrderByDescending(x => x.videoGames.Count).Take(amount);
        int i = 1;

        foreach (var item in t5cga)
        {
            Console.WriteLine($"{i}. {item.name} ({item.videoGames.Count})");
            i++;
        }


    }

    public async Task CreateSuperHeroSquad(int amount)
    {
        // create a superhero squad of most favored allies
        var mostFavoredAllies = disneyCharacters
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
