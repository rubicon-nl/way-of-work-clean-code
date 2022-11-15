namespace Rubicon.Wow.CleanCode.Example.UI;

public class CharacterPresenter : ICharacterPresenter
{
    private readonly ILogger<CharacterPresenter> logger;
    public CharacterPresenter(ILogger<CharacterPresenter> logger)
    {
        this.logger = logger;

    }

    public Task ShowTopMovieAppearances(IEnumerable<CharacterViewModel> characters)
    {
        int i = 1;

        foreach (var character in characters)
        {
            logger.LogInformation($"{i}. {character.Name} ({character.FilmsCount})");
            i++;
        }
        return Task.CompletedTask;
    }
}
