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
        var topCharacerMovieAppearances = disneyCharacters.OrderByDescending(x => x.films.Count).Take(amount);
        int i = 1;

        foreach (var character in topCharacerMovieAppearances)
        {
            Console.WriteLine($"{i}. {character.name} ({character.films.Count})");
            i++;
        }
    }

    public async Task TopGameAppearances(int amount)
    {
        var topCharacerGameAppearances = disneyCharacters.OrderByDescending(x => x.videoGames.Count).Take(amount);
        int i = 1;

        foreach (var character in topCharacerGameAppearances)
        {
            Console.WriteLine($"{i}. {character.name} ({character.videoGames.Count})");
            i++;
        }

    }

    public async Task CreateSuperHeroSquad(int amount)
    {
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
