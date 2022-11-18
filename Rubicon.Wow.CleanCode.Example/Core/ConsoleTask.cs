using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Infrastructure;

namespace Rubicon.Wow.CleanCode.Example.Core;

public class ConsoleTask : IConsoleTask
{
    private readonly IDisneyCharacterService _disneyCharacterService;
    private readonly IDisneyCharacterRepository _disneyCharacterRepository;
    private readonly IOutputWriter _outputWriter;

    public ConsoleTask(IOutputWriter outputWriter, IDisneyCharacterRepository disneyCharacterRepository)
    {
        _disneyCharacterRepository = disneyCharacterRepository;
        _outputWriter = outputWriter;
    }
    public async Task<bool> ExecuteAsync()
    {
        _outputWriter.WriteLine("Start fetching Disney characters");
        var retrievedCharacters = await _disneyCharacterRepository.GetAsync();
        _outputWriter.WriteLine();

        if (retrievedCharacters == null)
        {
            _outputWriter.WriteLine("No data retrieved");
            return false;
        }

        IDisneyCharacterService disneyCharacterService = new DisneyCharacterService(retrievedCharacters.AsQueryable());

        _outputWriter.WriteLine("Top 5 character movie appearances");
        disneyCharacterService.GetTopDisneyCharactersWithMostMovieAppeances(5);
        _outputWriter.WriteLine();

        _outputWriter.WriteLine("Top 5 character game appearances");
        disneyCharacterService.GetTopDisneyCharactersWithMostVideoGameAppeances(5);
        _outputWriter.WriteLine();
        _outputWriter.WriteLine("Create superhero squad of most favored allies");
        disneyCharacterService.GetMostFavoriteAllies(5);
        return true;
    }

}

