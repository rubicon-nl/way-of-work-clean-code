using AutoMapper;
using Rubicon.Wow.CleanCode.Example.Domain.DTO;

namespace Rubicon.Wow.CleanCode.Example.Domain;

/// <inheritdoc/>
public class DisneyCharacterService : IDisneyCharacterService
{
    private readonly List<DisneyCharacter> disneyCharacters;
    private readonly ILogger<DisneyCharacterService> logger;
    private readonly IMapper mapper;
    private readonly ICharacterPresenter presenter;

    public DisneyCharacterService(
        IDisneyCharacterRepository disneyCharacterRepository,
        ILogger<DisneyCharacterService> logger,
        IMapper mapper,
        ICharacterPresenter presenter)
    {
        this.mapper = mapper;
        this.presenter = presenter;
        this.disneyCharacters = disneyCharacterRepository.GetDisneyCharacters().Result;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task TopMovieAppearances(int amount)
    {
        var topCharacerMovieAppearances = disneyCharacters.OrderByDescending(x => x.films.Count).Take(amount);

        // map to dtos.
        var dtos = topCharacerMovieAppearances.Select(c => mapper.Map<DisneyCharacter, CharacterDTO>(c));

        await presenter.ShowTopMovieAppearances(dtos);
    }

    /// <inheritdoc/>
    public async Task TopGameAppearances(int amount)
    {

        var topCharacerGameAppearances = disneyCharacters.OrderByDescending(x => x.videoGames.Count).Take(amount);

        // map to dtos.
        var dtos = topCharacerGameAppearances.Select(c => mapper.Map<DisneyCharacter, CharacterDTO>(c));

        await presenter.ShowTopGameAppearances(dtos);
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
}
