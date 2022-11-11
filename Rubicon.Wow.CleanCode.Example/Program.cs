using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Infrastructure;

Console.WriteLine("Start fetching Disney characters");

var disneyCharacterRepository = new DisneyCharacterRepository();
var disneyCharacters = await disneyCharacterRepository.GetDisneyCharacters();

Console.WriteLine("Top 5 character movie appearances");

var disneyCharacterService = new DisneyCharacterService();
await disneyCharacterService.TopMovieAppearances(disneyCharacters, 5);

Console.WriteLine("Top 5 character game appearances");

await disneyCharacterService.TopGameAppearances(disneyCharacters, 5);

Console.WriteLine("Create superhero squad of most favored allies");

await disneyCharacterService.CreateSuperHeroSquad(disneyCharacters, 4);