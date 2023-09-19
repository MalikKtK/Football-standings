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

                Team team = new Team();

                try
                {
                    team.Abbreviation = fields[0];
                    team.FullName = fields[1];
                    team.SpecialRanking = string.IsNullOrEmpty(fields[2]) ? string.Empty : fields[2].Trim();
                    team.Position = int.Parse(fields[3]);
                    team.GamesPlayed = int.Parse(fields[4]);
                    team.GamesWon = int.Parse(fields[5]);
                    team.GamesDrawn = int.Parse(fields[6]);
                    team.GamesLost = int.Parse(fields[7]);
                    team.GoalsFor = int.Parse(fields[8]);
                    team.GoalsAgainst = int.Parse(fields[9]);
                    team.Points = int.Parse(fields[10]);
                    team.CurrentStreak = fields.Length > 11 ? fields[11] : string.Empty;

                    teams.Add(team);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {string.Join(",", fields)}");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return teams;
    }
}
