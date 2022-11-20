using Rubicon.Wow.CleanCode.Example.Domain;

namespace Rubicon.Wow.CleanCode.Example;

internal class DoStuff : BackgroundService
{
    private readonly IDisneyCharacterService disneyCharacterService;
    private readonly ILogger<DoStuff> logger;

    public DoStuff(IDisneyCharacterService disneyCharacterService, ILogger<DoStuff> logger)
    {
        this.disneyCharacterService = disneyCharacterService;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Top 5 character movie appearances");

        await disneyCharacterService.TopMovieAppearances(5);

        logger.LogInformation("Top 5 character game appearances");

        await disneyCharacterService.TopGameAppearances(5);

        logger.LogInformation("Create superhero squad of most favored allies");

        await disneyCharacterService.CreateSuperHeroSquad(4);

        await disneyCharacterService.StoreCharacter(new DisneyCharacter()
        {
            name = "Bamiebal",
            imageUrl = "someUrl"
        });
    }
}
