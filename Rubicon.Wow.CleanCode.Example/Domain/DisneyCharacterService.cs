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

    internal Task CreateSuperHeroSquad(List<DisneyCharacter> cumulatedCharacters, int v)
    {
        throw new NotImplementedException();
    }
}
