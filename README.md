# Football Standing

Dette er vores første Mandatory projekt for C#. Vi har et dansk superliga, som indeholder 12 hold. Hvert år bliver der spillet cirka 22 kampe pr. hold (2 kampe mod alle hold).

I slutningen af sæsonnen vil det være det hold med mest points, der både vinder supaligaen og videre til championsleague. Derefter er de to næste hold i tabellen videre til El, som står for Europa League og derefter er de tre næste kvalificeret til Europa Cup. De tre sidste hold i tabellen er til nedrykning fra super ligaen. 

Hver gang et hold vinder, får holdet 3 points, hvor uafgjort gir 1 og tabt får man ikke point. 

## Indhold
- [Instillation](#Instillation)
- [Brug](#Brug)
- [Configuration](#configuration)
- [Konfiguration](#Konfiguration)
- [Kontakt](#Kontakt)

## Instillation 

Du skal have NET 7 for at køre programmet samt en enhed der har et software til at write, compile, and run og C#/.NET application. Der anbefaldes at der bliver brugt Visual Studio Code.

## Brug

Du skal åbne en terminal op, og til at starte med skrive følgende:
 - dotnet clean (Til at fjerne caches)
 - dotnet build (Til at bygge projektet op)
 - dotnet run (Til at starte projektet op)

 Herefter har du 5 valgmuligheder.
1. Print Teams (Printe holdene ud)
2. Run Matches (Køre kampene manualt (eksempelvis round-01.csv))
3. Display Current League Standings (Printe tabellen af ligaen ud)
4. Simulate all matches  (Simulaterer alle vores kampe, så den færddigøre hele sæsonnen.)
5. Exit (Afslutter programmet)

Der skal huskes, at selvom der står en score i vores csv filer, er det stadig udgivet randomt. Dvs. hver gang du kører manualt en kamp eller en hel sæson, vil resultatet altid være randomt. 

## Konfiguration

Sørg for at setup.csv og teams.csv bliver loadet ind. Hvis de ikke gør, virker programmet ikke. Ellers er der ikke andre konfigurationer der er behov for, udover at huske at have NET 7

## Kontakt

- Malik Kütük
- Email: mali0237@stud.kea.dk

- Ebubekir Kayhan
- Email: ebub0036@stud.kea.dk
