Jeffrey
--------------

1. Seperation of concerns (modulair maken, opbreken problemen, beheerbaarheid op lange termijn)
2. Dependency Injection (ontkoppeling van elkaar)
3. Patterns and practices
    1. Error handling
    2. Guard pattern
    3. Naming & comments

Rene
--------------

4. Logging
5. Polly
6. Presenter
7. Fluent Validation

Dieder
--------------

8. Unit testen
8b. Repository pattern
8c. Fluent Assertions

-> Data in eigen project. Voorkeur dat Enitities met records worden gedeinieerd omdat deze immutable zijn.
-> Serialisation class gemaakt die verantwoordlijk is voor goede serializatie. Zorgt er voor dat camelcasing json goed geimporteerd wordt.
-> Business logica naar een service class gebracht. Zodat dit de veranntwoordelijkheid is van de servcie.
-> Self hosting service
-> http client to factory pattern en decorator voor disposable interface
-> Disney service via interface
-> envtueel logging

## Script Jeffrey

1. Seperation of concerns (modulair maken, opbreken problemen, beheerbaarheid op lange termijn)
    1. Data project maken en DisneyCharacter.cs maken en code verplaatsen.
    2. Domain folder maken en DisneyCharacterService.cs maken en code verplaatsen.
    3. Infrastructure folder maken en DisneyCharacterRepository.cs maken en code verplaatsen.
    4. Program.cs fatsoeneren
2. Dependency Injection (ontkoppeling van elkaar)
    1. DisneyCharacterRepository logica van program.cs verplaatsen naar de service constructor.
    2. Maak DoStuff.cs en inherit BackgroundService
    3. Voeg snippet toe aan program.cs

    ```c#
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) => services.AddHostedService<DoStuff>())
    .Build()
    .RunAsync();
    ```

    4. Interfaces maken voor de Services en Repositories en injecten (je wilt niet overal nieuwe objecten maken, maar 1x maken en hergebruiken.)
    5. ConfigureServices aanvullen met singletons
3. Patterns and practices
    1. Error handling
        1. Url veranderen naar iets niets-bestaand zorgt ervoor dat de applicatie zonder duidelijke melding klapt.
        2. `httpResponse.EnsureSuccessStatusCode()` toevoegen en try-catch met error loggen naar console.
    2. Guard pattern
        1. Try-catch verkleinen
        2. IsSuccessStatusCode opruimen
        3. if httpResponse.Content omdraaien
        4. JsonException opruimen
        5. ArgumentNullException op characters.
        6. Aparte methode voor ophalen page.
    3. Naming & comments
        1. t5cma naamgeving naar topCharacterMovieAppearances
        2. Comments kloppen niet. Ook niet nodig. Weghalen.

## Script Rene

4. Logging (ontkoppelen van console en gebruik maken van andere logging providers)
    1. Voeg `ILogger<DoStuff> logger` toe aan DoStuff.cs (using Microsoft.Extensions.Logging)
    2. Console.WriteLine naar logger.LogInformation
    3. Laten zien dat dit direct werkt (zonder loggingproviders etc.)
    4. Console.WriteLines in DisneyCharacterService.cs omzetten naar logInformation's
    5. Console.WriteLine in de repository omzetten naar *logTrace* en laten zien dat de output niet meer in de console komt.
    6. Voeg appsettings.json toe en zet de CopyToOutputDirectory daarvan op Always

    ```json
    {
    "ServiceBus": {
        "Namespace": "sb-scanplan-tst",
        "Queue": "geoslam"
    },
    "ApplicationinsightsConnectionString": "",
    "Logging": {
        "LogLevel": {
        "Default": "Trace",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
        }
    }
    }
    ```

    7. Start app nogmaals en laat zien dat Trace nu wel getoond wordt.
    8. Voeg nuget package `microsoft.extensions.logging.applicationinsights` toe aan project
    9. Voeg volgende snippet toe voor `.ConfigureServices` in Program.cs.
    _JP:Let op, hier hoort nog een configuratie bij in de appsettings.json, nog geen tijd gehad om dit te testen_

    ```c#
    .ConfigureLogging(builder => builder.AddApplicationInsights())
    ```
5.  Polly
    1. Voeg Polly packages toe aan project
        Microsoft.Extensions.Http.Polly
        Polly.Contrib.WaitAndRetry

    2.  Voeg code snippet toe services in Program.cs voor het registreren van een httpClient inclusief retry policy.
    ```c#
    services.AddHttpClient<IDisneyCharacterRepository, DisneyCharacterRepository>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddPolicyHandler(GetRetryPolicy());

    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(delay);
    }
    ```

    3.  injecteer httpClient in constructor van repository voor het ophalen van de characters.
    ```c#
    public DisneyCharacterRepository(ILogger<DisneyCharacterRepository> logger, HttpClient httpClient)
    {
        this.client = httpClient;
        this.logger = logger;
    }
    ```