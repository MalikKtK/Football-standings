# Football Standings

Dette er vores første obligatoriske projekt for C#. Vi har en dansk Superliga, som indeholder 12 hold. Hver sæson spilles der cirka 22 kampe pr. hold (2 kampe per modstander - 1 ude- og hjemmekamp).

I slutningen af sæsonen vil det hold med flest point både vinde Superligaen og kvalificere sig til Champions League. Derefter går de næste to hold på ranglisten videre til Europa League, og de efterfølgende tre kvalificerer sig til Conference League. De tre pladser i tabel-stillingen rykker ned fra Superligaen.

Hver gang et hold vinder, optjener holdet 3 point, mens uafgjort giver 1 point, og ved tab opnår man ingen point.

## Indhold

- [Installation](#Instillation)
- [Brug](#Brug)
- [Konfiguration](#Konfiguration)
- [Credits](#Credits)
- [Kontakt](#Kontakt)

## Installation

Du skal have .NET 7 for at køre programmet samt en enhed, der har software til at skrive, kompilere og køre en C#/.NET-applikation. Det anbefales at bruge Visual Studio Code.

## Brug

Du skal åbne en terminal og indtaste følgende for at starte:

    dotnet clean (For at fjerne cache)
    dotnet build (For at bygge projektet)
    dotnet run (For at starte projektet)

Derefter har du 5 valgmuligheder:

1. Print Teams (Printe holdene ud)
2. Run Matches (Spiller kampe manuelt (eksempelvis kan bruger-input være: round-01.csv))
3. Display Current League Standings (Printe tabellen af ligaen ud)
4. Simulate All Matches (Simulation af alle kampe. Dette resultere i, at alle kampe færdigspilles og så kan man derefter se den endelige tabel-stilling)
5. Exit (Afslutter programmet)

Husk, at selvom der står en score i vores CSV-filer, er resultatet stadig tilfældigt. Dvs. hver gang du manuelt spiller en kamp eller en hel sæson, vil resultatet altid være tilfældigt.

## Konfiguration

Sørg for at indlæse setup.csv og teams.csv. Hvis de ikke indlæses, fungerer programmet ikke. Ellers er der ingen yderligere konfigurationer, der er nødvendige, udover at have .NET 7 installeret.

## Credits

I vores projekt er der en LINKS.MD-fil. Her kan du finde links til de hjælpemidler vi har brugt, samt chats med ChatGPT om fejl, der er opstået undervejs.

## Kontakt

- Malik Kütük
- Email: mali0237@stud.kea.dk

- Ebubekir Kayhan
- Email: ebub0036@stud.kea.dk
