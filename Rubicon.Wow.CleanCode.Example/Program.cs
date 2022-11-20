using System.Reflection;
using AutoMapper;
using FluentValidation;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Rubicon.Wow.CleanCode.Example;
using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Infrastructure;
using Rubicon.Wow.CleanCode.Example.UI;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        services.AddHttpClient<IDisneyCharacterRepository, DisneyCharacterRepository>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
                    .AddPolicyHandler(GetRetryPolicy());

        services.AddSingleton<IDisneyCharacterRepository, DisneyCharacterRepository>();
        services.AddSingleton<IDisneyCharacterService, DisneyCharacterService>();
        services.AddHostedService<DoStuff>();

        // register automapper profiles.
        services.AddAutoMapper(Assembly.GetAssembly(typeof(CharacterProfile)));

        // register presenters
        services.AddScoped<ICharacterPresenter, CharacterPresenter>();

        // register validators
        services.AddScoped<IValidator<DisneyCharacter>, DisneyCharacterValidator>();
    })
    .Build()
    .RunAsync();

IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(delay);
}