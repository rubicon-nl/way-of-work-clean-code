using AutoMapper;
using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Domain.DTO;

namespace Rubicon.Wow.CleanCode.Example.UI;

/// <inheritdoc/>
public class CharacterPresenter : ICharacterPresenter
{
    private readonly ILogger<CharacterPresenter> logger;
    private readonly IMapper mapper;
    public CharacterPresenter(ILogger<CharacterPresenter> logger, IMapper mapper)
    {
        this.mapper = mapper;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public Task ShowTopMovieAppearances(IEnumerable<CharacterDTO> characters)
    {
        int i = 1;

        foreach (var character in characters)
        {
            logger.LogInformation($"{i}. {character.Name} ({character.FilmsCount})");
            i++;
        }
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task ShowTopGameAppearances(IEnumerable<CharacterDTO> characters)
    {
        int i = 1;

        foreach (var character in characters)
        {
            logger.LogInformation($"{i}. {character.Name} ({character.VideogamesCount})");
            i++;
        }
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
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
