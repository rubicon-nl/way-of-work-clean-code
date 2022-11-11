Jeffrey
--------------

1. Seperation of concerns: Structure program into classes
1b. Data in eigen project

2. Dependency Injection
2.B Self hosting
2.C Interfaces maken

3
3A HTTP error (change url to non-existing url) causes hanging in a while loop. Introduce error throwing.
3B Guard pattern, reduce nested loops and ifs.
3C Variable naming: t5cma -> top5CharacterMovieAppearances
3D Comments lie -> // find top 5 disney characters with most movie appearances

Rene
--------------

4 Polly
5 Presenter
6 Logging
7 Fluent Validation

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
        1. 


3
3A HTTP error (change url to non-existing url) causes hanging in a while loop. Introduce error throwing.
3B Guard pattern, reduce nested loops and ifs.
3C Variable naming: t5cma -> top5CharacterMovieAppearances
3D Comments lie -> // find top 5 disney characters with most movie appearances