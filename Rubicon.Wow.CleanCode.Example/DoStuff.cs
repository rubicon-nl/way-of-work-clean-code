using Rubicon.Wow.CleanCode.Example.Domain;

namespace Rubicon.Wow.CleanCode.Example;

internal class DoStuff : BackgroundService
{
    private readonly IDisneyCharacterService disneyCharacterService;

    public DoStuff(IDisneyCharacterService disneyCharacterService)
    {
        this.disneyCharacterService = disneyCharacterService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Top 5 character movie appearances");

        await disneyCharacterService.TopMovieAppearances(5);

        Console.WriteLine("Top 5 character game appearances");

        await disneyCharacterService.TopGameAppearances(5);

        Console.WriteLine("Create superhero squad of most favored allies");

        await disneyCharacterService.CreateSuperHeroSquad(4);
    }
}
