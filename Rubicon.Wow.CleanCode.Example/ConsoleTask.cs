namespace Rubicon.Wow.CleanCode.Example;
public class ConsoleTask : IConsoleTask
{
    private readonly IDisneyCharacterService _disneyCharacterService;
    private readonly IOutputWriter _outputWriter;

    public ConsoleTask(IDisneyCharacterService disneyCharacterService, IOutputWriter outputWriter)
    {
        _disneyCharacterService = disneyCharacterService;
        _outputWriter = outputWriter;
    }
    public async Task<bool> ExecuteAsync()
    {
        _outputWriter.WriteLine("Start fetching Disney characters");
        await _disneyCharacterService.FetchCharactersAsync();
        _outputWriter.WriteLine();

        _outputWriter.WriteLine("Top 5 character movie appearances");
        _disneyCharacterService.GetTopDisneyCharactersWithMostMovieAppeances(5);
        _outputWriter.WriteLine();

        _outputWriter.WriteLine("Top 5 character game appearances");
        _disneyCharacterService.GetTopDisneyCharactersWithMostVideoGameAppeances(5);
        _outputWriter.WriteLine();
        _outputWriter.WriteLine("Create superhero squad of most favored allies");
        _disneyCharacterService.GetMostFavoriteAllies(5);
        return true;
    }

}

