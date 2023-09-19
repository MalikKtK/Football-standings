using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Users\kutuk\Football standings\FootballStandings\Data\teams.csv";

        List<Team> teams = FootballCsvReader.ReadCsv(filePath);

        foreach (var team in teams)
        {
            Console.WriteLine($"Abbreviation: {team.Abbreviation}");
            Console.WriteLine($"Full Name: {team.FullName}");
            Console.WriteLine($"Special Ranking: {team.SpecialRanking}");
            Console.WriteLine($"Position: {team.Position}");
            Console.WriteLine($"Games Played: {team.GamesPlayed}");
            Console.WriteLine($"Games Won: {team.GamesWon}");
            Console.WriteLine($"Games Drawn: {team.GamesDrawn}");
            Console.WriteLine($"Games Lost: {team.GamesLost}");
            Console.WriteLine($"Goals For: {team.GoalsFor}");
            Console.WriteLine($"Goals Against: {team.GoalsAgainst}");
            Console.WriteLine($"Goal Difference: {team.GoalDifference}");
            Console.WriteLine($"Points: {team.Points}");
            Console.WriteLine($"Current Streak: {team.CurrentStreak}");
            Console.WriteLine();
        }
    }
}
