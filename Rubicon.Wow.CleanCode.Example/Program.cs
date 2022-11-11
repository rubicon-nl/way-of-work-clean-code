using Rubicon.Wow.CleanCode.Example;
using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Infrastructure;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) => {
        services.AddSingleton<IDisneyCharacterRepository, DisneyCharacterRepository>();
        services.AddSingleton<IDisneyCharacterService, DisneyCharacterService>();
        services.AddHostedService<DoStuff>();
    })
    .Build()
    .RunAsync();