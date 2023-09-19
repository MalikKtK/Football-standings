using System;
using System.Collections.Generic;
using System.Linq;

public class LeagueStandings
{
    private List<Team> teams;

    public LeagueStandings(List<Team> teams)
    {
        this.teams = teams;
    }

    public List<Team> CalculateStandings()
    {
        var orderedStandings = teams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => team.GoalDifference)
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ToList();

        for (int i = 0; i < orderedStandings.Count; i++)
        {
            orderedStandings[i].Position = i + 1;
        }

        return orderedStandings;
    }

    public void PrintStandings()
    {
        var standings = CalculateStandings();

        Console.WriteLine("League Standings:");
        Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}",
                          "Pos", "Team", "Pts", "GP", "W", "D", "L", "GF", "GA", "GD", "Streak");

        foreach (var team in standings)
        {
            Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}",
                              team.Position, team.FullName, team.Points, team.GamesPlayed,
                              team.GamesWon, team.GamesDrawn, team.GamesLost, team.GoalsFor,
                              team.GoalsAgainst, team.GoalDifference, team.CurrentStreak);
        }
    }
}
