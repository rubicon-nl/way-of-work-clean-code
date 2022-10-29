namespace Rubicon.Wow.CleanCode.Example;
public class ConsoleTask : IConsoleTask
{
    private readonly IDisneyCharacterService _disneyCharacterService;
    public ConsoleTask(IDisneyCharacterService disneyCharacterService)
    {
        this._disneyCharacterService = disneyCharacterService;
    }
    public async Task<bool> ExecuteAsync()
    {        
        Console.WriteLine("Start fetching Disney characters");
        await _disneyCharacterService.FetchCharacters();
        Console.WriteLine();

        Console.WriteLine("Top 5 character movie appearances");
        _disneyCharacterService.GetTopDisneyCharactersWithMostMovieAppeances(5);
        Console.WriteLine();

        Console.WriteLine("Top 5 character game appearances");
        _disneyCharacterService.GetTopDisneyCharactersWithMostVideoGameAppeances(5);
        Console.WriteLine();
        Console.WriteLine("Create superhero squad of most favored allies");
        _disneyCharacterService.GetMostFavoriteAllies(5);
        return true;
    }

}

