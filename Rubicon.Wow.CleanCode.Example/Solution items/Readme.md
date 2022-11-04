##Script

Jeffrey
--------------
1. Seperation of concerns: Structure program into classes
1b. Data in eigen project

2. Dependency Injection
2.B Self hosting
2.C Interfaces maken

/* TODO */
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