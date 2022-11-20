using AutoMapper;
using FluentValidation;

namespace Rubicon.Wow.CleanCode.Example.Domain;

/// <inheritdoc/>
public class DisneyCharacterService : IDisneyCharacterService
{
    private readonly List<DisneyCharacter> disneyCharacters;
    private readonly ILogger<DisneyCharacterService> logger;
    private readonly IMapper mapper;
    private readonly ICharacterPresenter presenter;
    private readonly IValidator<DisneyCharacter> validator;

    public DisneyCharacterService(
        IDisneyCharacterRepository disneyCharacterRepository,
        ILogger<DisneyCharacterService> logger,
        IMapper mapper,
        ICharacterPresenter presenter,
        IValidator<DisneyCharacter> validator)
    {
        this.mapper = mapper;
        this.presenter = presenter;
        this.validator = validator;
        this.disneyCharacters = disneyCharacterRepository.GetDisneyCharacters().Result;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task TopMovieAppearances(int amount)
    {
        var topCharacerMovieAppearances = disneyCharacters.OrderByDescending(x => x.films.Count).Take(amount);

        await presenter.ShowTopMovieAppearances(topCharacerMovieAppearances);
    }

    /// <inheritdoc/>
    public async Task TopGameAppearances(int amount)
    {
        var topCharacerGameAppearances = disneyCharacters.OrderByDescending(x => x.videoGames.Count).Take(amount);

        await presenter.ShowTopGameAppearances(topCharacerGameAppearances);
    }

    /// <inheritdoc/>
    public async Task CreateSuperHeroSquad(int amount)
    {
        var mostFavoredAllies = disneyCharacters
            .SelectMany(x => x.allies)
            .GroupBy(x => x)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Select(x => x.Name)
            .Take(amount);

        await presenter.ShowSuperHeroSquad(mostFavoredAllies);
    }

    public Task StoreCharacter(DisneyCharacter character)
    {
        var validationResult = validator.Validate(character);
        if (!validationResult.IsValid)
        {
            logger.LogError($"Validation error when store character, errors:");
            foreach (var error in validationResult.Errors)
            {
                logger.LogError($"ErrorCode: {error.ErrorCode}, ErrorMessage: {error.ErrorMessage}");
            }
            return Task.CompletedTask;
        }
        logger.LogInformation("We can invoke repository to store character.");
        return Task.CompletedTask;
    }
}
