##Script

Jeffrey
--------------
1. Seperation of concerns: Structure program into classes
1b. Data in eigen project

2. Dependency Injection
2.B Self hosting
2.C Interfaces maken

/* TODO */
4
4A HTTP error (change url to non-existing url) causes hanging in a while loop. Introduce error throwing.
4B Guard pattern, reduce nested loops and ifs.
4C Variable naming: t5cma -> top5CharacterMovieAppearances
4D Comments lie -> // find top 5 disney characters with most movie appearances

Rene
--------------
Polly
Presenter
Logging
Fluent Validation

Dieder
--------------
3. Unit testen
3b. Repository pattern
3c. Fluent Assertions




-> Data in eigen project. Voorkeur dat Enitities met records worden gedeinieerd omdat deze immutable zijn.
-> Serialisation class gemaakt die verantwoordlijk is voor goede serializatie. Zorgt er voor dat camelcasing json goed geimporteerd wordt.
-> Business logica naar een service class gebracht. Zodat dit de veranntwoordelijkheid is van de servcie.
-> Self hosting service
-> http client to factory pattern en decorator voor disposable interface
-> Disney service via interface
-> envtueel logging