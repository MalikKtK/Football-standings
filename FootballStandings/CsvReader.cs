using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

public class FootballCsvReader
{
    public static List<Team> ReadCsv(string filePath)
    {
        List<Team> teams = new List<Team>();

        using (TextFieldParser parser = new TextFieldParser(filePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            // Skip the header line
            parser.ReadLine();

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();

                if (fields.Length != 12)
                {
                    Console.WriteLine($"Skipping invalid line: {string.Join(",", fields)}");
                    continue;
                }

                Team team = new Team
                {
                    Abbreviation = fields[0],
                    FullName = fields[1],
                    SpecialRanking = fields[2],
                    Position = int.Parse(fields[3]),
                    GamesPlayed = int.Parse(fields[4]),
                    GamesWon = int.Parse(fields[5]),
                    GamesDrawn = int.Parse(fields[6]),
                    GamesLost = int.Parse(fields[7]),
                    GoalsFor = int.Parse(fields[8]),
                    GoalsAgainst = int.Parse(fields[9]),
                    Points = int.Parse(fields[10]),
                    CurrentStreak = fields[11]
                };

                teams.Add(team);
            }
        }

        return teams;
    }
}
