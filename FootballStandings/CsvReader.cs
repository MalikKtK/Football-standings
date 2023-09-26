using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

public class FootballCsvReader
{
    public static List<Team> ReadCsv(string filePath)
    {
        //Create a list to store teams
        List<Team> teams = new List<Team>();

        //TextFieldParser to read the CSV file
        using (TextFieldParser parser = new TextFieldParser(filePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadLine();

            //Loop through each line in the CSV file
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();

                if (fields.Length != 12)
                {
                    Console.WriteLine($"Skipping invalid line: {string.Join(",", fields)}");
                    continue;
                }

                //Create a new Team object to store the parsed data
                Team team = new Team();

                try
                {
                    //Parse and assign values to Team properties
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

                    //Parse the streak data and assign it to a team's CurrentStreak
                    string streak = fields[11];
                    if (!string.IsNullOrEmpty(streak) && streak.Length >= 2)
                    {
                        char streakResult = streak[0];
                        int streakValue = int.Parse(streak.Substring(1));

                        if (streakResult == 'W')
                        {
                            team.CurrentStreak.Wins = streakValue;
                        }
                        else if (streakResult == 'D')
                        {
                            team.CurrentStreak.Draws = streakValue;
                        }
                        else if (streakResult == 'L')
                        {
                            team.CurrentStreak.Losses = streakValue;
                        }
                    }
                    else
                    {
                        //create a new empty streak object if the streak data is invalid
                        team.CurrentStreak = new Team.Streak();
                    }

                    teams.Add(team);
                }
                catch (Exception ex)
                {
                    //If an error occurs, print the error message and the line that caused the error
                    Console.WriteLine($"Error parsing line: {string.Join(",", fields)}");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return teams;
    }
}
