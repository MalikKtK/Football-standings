using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FootballProcessor
{
    private List<Team> teams;

    public FootballProcessor(List<Team> teams)
    {
        this.teams = teams;
    }

    public void ProcessRoundResults(string roundFilePath)
    {
        List<MatchResult> matchResults = MatchResultProcessor.ReadMatchResults(roundFilePath);

        foreach (var matchResult in matchResults)
        {
            Team homeTeam = teams.Find(team => team.Abbreviation == matchResult.HomeTeam);
            Team awayTeam = teams.Find(team => team.Abbreviation == matchResult.AwayTeam);

            if (homeTeam != null && awayTeam != null)
            {
                // Update team statistics based on match result
                UpdateTeamStatistics(homeTeam, awayTeam, matchResult);
            }
        }

        // Calculate and update the standings after processing the round
        CalculateStandings();
    }

    private void UpdateTeamStatistics(Team homeTeam, Team awayTeam, MatchResult matchResult)
    {
        homeTeam.GamesPlayed++;
        awayTeam.GamesPlayed++;
        homeTeam.GoalsFor += matchResult.HomeTeamGoals;
        homeTeam.GoalsAgainst += matchResult.AwayTeamGoals;
        awayTeam.GoalsFor += matchResult.AwayTeamGoals;
        awayTeam.GoalsAgainst += matchResult.HomeTeamGoals;

        // Update points, games won, games drawn, games lost, etc. based on the match result
        // Add your logic here to calculate points and update other statistics

        // Update the current streak for both teams
        // Add your logic here to update the streak

        // You may need to add more logic based on your specific requirements
    }

    private void CalculateStandings()
    {
        var orderedStandings = teams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => team.GoalDifference)
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ToList();

        for (int i = 0; i < orderedStandings.Count; i++)
        {
            orderedStandings[i].Position = i + 1;
        }
    }
}
