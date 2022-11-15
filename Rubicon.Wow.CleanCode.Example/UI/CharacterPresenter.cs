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

    public Task ShowTopGameAppearances(IEnumerable<CharacterViewModel> characters)
    {
        int i = 1;

        foreach (var character in characters)
        {
            logger.LogInformation($"{i}. {character.Name} ({character.VideogamesCount})");
            i++;
        }
        return Task.CompletedTask;
    }

    public Task ShowSuperHeroSquad(IEnumerable<string> names)
    {
        if (names != null)
        {
            foreach (var item in names)
            {
                logger.LogInformation($"{item}");
            }
        }
        return Task.CompletedTask;
    }
}
