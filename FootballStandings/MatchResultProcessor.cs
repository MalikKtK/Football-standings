using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

public class MatchResultProcessor
{
    //Reads match results from a CSV file and returns a list of MatchResult objects.
    public static List<MatchResult> ReadMatchResults(string filePath)
    {
        List<MatchResult> matchResults = new List<MatchResult>();

        
        using (TextFieldParser parser = new TextFieldParser(filePath))
        {
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadLine();

            while (!parser.EndOfData)
            {
                //Read a line and split it into fields based on the delimiter (',' in this case).
                string[] fields = parser.ReadFields();

                if (fields.Length != 5)
                {
                    Console.WriteLine($"Skipping invalid match result: {string.Join(",", fields)}");
                    continue;
                }

                //Extract data from the fields.
                string homeTeam = fields[0];
                string awayTeam = fields[1];

                //Split the goal values (e.g., "2-1") and parse them as integers.
                string[] goalValues = fields[2].Split('-');
                if (goalValues.Length != 2 || !int.TryParse(goalValues[0], out int homeTeamGoals) || !int.TryParse(goalValues[1], out int awayTeamGoals))
                {
                    Console.WriteLine($"Skipping invalid goal values: {fields[2]}");
                    continue;
                }

                //Extract the match date.
                string matchDate = fields[4];

                //Create a MatchResult object and add it to the list.
                MatchResult matchResult = new MatchResult(homeTeam, awayTeam, homeTeamGoals, awayTeamGoals, matchDate, string.Empty);
                matchResults.Add(matchResult);
            }
        }

        return matchResults;
    }
}
