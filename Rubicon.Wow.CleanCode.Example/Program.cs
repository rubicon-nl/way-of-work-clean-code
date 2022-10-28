//var builder = Host.CreateDefaultBuilder(args);

//builder.ConfigureServices(services => 
//        services
//            .AddHostedService<Worker>()
//            .AddScoped<IMessageWriter, MessageWriter>());

//var host = builder.Build();

//host.Run();


using Rubicon.Wow.CleanCode.Example;

DisneyCharacterService service = new DisneyCharacterService();
Console.WriteLine("Start fetching Disney characters");
await service.FetchCharacters();
Console.WriteLine();

Console.WriteLine("Top 5 character movie appearances");
service.GetTopDisneyCharactersWithMostMovieAppeances(5);
Console.WriteLine();

Console.WriteLine("Top 5 character game appearances");
service.GetTopDisneyCharactersWithMostVideoGameAppeances(5);
Console.WriteLine();
Console.WriteLine("Create superhero squad of most favored allies");
service.GetMostFavoriteAllies(5);

