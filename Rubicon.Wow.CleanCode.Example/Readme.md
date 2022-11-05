##Script

1. Seperation of concerns: Structure program into classes
2. Dependency Injection
3. HTTP error (change url to non-existing url) causes hanging in a while loop. Introduce error throwing.
4. Guard pattern, reduce nested loops and ifs.
5. Variable naming: t5cma -> top5CharacterMovieAppearances
6. Comments lie -> // find top 5 disney characters with most movie appearances

-> Data in eigen project. Voorkeur dat Enitities met records worden gedeinieerd omdat deze immutable zijn.
-> Serialisation class gemaakt die verantwoordlijk is voor goede serializatie. Zorgt er voor dat camelcasing json goed geimporteerd wordt.
-> Business logica naar een service class gebracht. Zodat dit de veranntwoordelijkheid is van de servcie.
-> Self hosting service
-> http client to factory pattern en decorator voor disposable interface
-> Disney service via interface
-> envtueel logging

----
Test
8

:) Naamgeving unittest is aangepast
 -> ExecuteAsync_ShouldCallProcessingFunctions_AndReturnsTrue(
:) Gebruik van mocks gemaakt
:) Opbouw Test aangepast

-> Unittest is alleen testen van een class
-> Alleen getest dat ik true terug krijg. Nu iets beter, want we testen 1 resultaat naam.
-> Setup/Arrange => onafhankelijk/Isolated test voor andere testen? (HttpClient, HttpClientFactory, DisneyService). 
-> Test bevat teveel verantwoordleijkheden
-> Veel te veel test data en moeilijk aan te passen aan nieuwe situaties

-> Test geeft aan dat het goed gaat, maar test in feite alleen maar dat er geen unhandled exceptie optreden
-> enz..