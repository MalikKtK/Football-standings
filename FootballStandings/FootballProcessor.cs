using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FootballProcessor
{
    private List<Team> teams;
    private LeagueSetup leagueSetup; // Private field to store the LeagueSetup instance

    public FootballProcessor(List<Team> teams, LeagueSetup leagueSetup)
    {
        this.teams = teams;
        this.leagueSetup = leagueSetup; // Store the LeagueSetup instance
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
        UpdatePoints(homeTeam, awayTeam, matchResult);

        // Update the current streak for both teams
        UpdateStreak(homeTeam, awayTeam, matchResult);

        // You may need to add more logic based on your specific requirements
    }

    private void UpdatePoints(Team homeTeam, Team awayTeam, MatchResult matchResult)
    {
        // Add your logic to calculate points based on match result
        if (matchResult.HomeTeamGoals > matchResult.AwayTeamGoals)
        {
            homeTeam.Points += 3; // Home team wins
            homeTeam.GamesWon++;
            awayTeam.GamesLost++;
        }
        else if (matchResult.HomeTeamGoals < matchResult.AwayTeamGoals)
        {
            awayTeam.Points += 3; // Away team wins
            awayTeam.GamesWon++;
            homeTeam.GamesLost++;
        }
        else
        {
            homeTeam.Points += 1; // It's a draw
            awayTeam.Points += 1;
            homeTeam.GamesDrawn++;
            awayTeam.GamesDrawn++;
        }
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


private void UpdateStreak(Team homeTeam, Team awayTeam, MatchResult matchResult)
{
    // Determine the match outcome
    bool homeTeamWon = matchResult.HomeTeamGoals > matchResult.AwayTeamGoals;
    bool awayTeamWon = matchResult.AwayTeamGoals > matchResult.HomeTeamGoals;
    bool isDraw = matchResult.HomeTeamGoals == matchResult.AwayTeamGoals;

    // Update home team's streak
    if (homeTeamWon)
    {
        // Home team won, update the streak
        homeTeam.CurrentStreak.Wins++;
    }
    else if (isDraw)
    {
        // It's a draw for the home team, update the streak
        homeTeam.CurrentStreak.Draws++;
    }
    else
    {
        // Home team lost, update the streak
        homeTeam.CurrentStreak.Losses++;
    }

    // Update away team's streak
    if (awayTeamWon)
    {
        // Away team won, update the streak
        awayTeam.CurrentStreak.Wins++;
    }
    else if (isDraw)
    {
        // It's a draw for the away team, update the streak
        awayTeam.CurrentStreak.Draws++;
    }
    else
    {
        // Away team lost, update the streak
        awayTeam.CurrentStreak.Losses++;
    }
}


 public void DisplayCurrentStandings()
{
    var orderedStandings = teams.OrderByDescending(team => team.Points)
                                .ThenByDescending(team => team.GoalDifference)
                                .ThenByDescending(team => team.GoalsFor)
                                .ToList();

    Console.WriteLine("League Standings:");
    Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}",
                      "Pos", "Team", "Pts", "GP", "W", "D", "L", "GF", "GA", "GD", "Streak");

    foreach (var team in orderedStandings)
    {
        string specialMarking = "";

        // Add special marking for CL, EL, EC qualification
        if (team.Position <= leagueSetup.PromoteToChampionsLeague)
            specialMarking = "(CL)";
        else if (team.Position <= (leagueSetup.PromoteToChampionsLeague + leagueSetup.PromoteToEuropeLeague))
            specialMarking = "(EL)";
        else if (team.Position <= (leagueSetup.PromoteToChampionsLeague + leagueSetup.PromoteToEuropeLeague + leagueSetup.PromoteToConferenceLeague))
            specialMarking = "(EC)";

        // Add coloring for relegation-threatened teams
        string textColor = "white";
        if (team.Position >= teams.Count - leagueSetup.RelegateToLowerLeague)
            textColor = "red";

        string streakText = $"Wins: {team.CurrentStreak.Wins}, Draws: {team.CurrentStreak.Draws}, Losses: {team.CurrentStreak.Losses}";
        Console.WriteLine($"<color={textColor}>{team.Position,-5} {specialMarking} {team.FullName,-25} {team.Points,-10} {team.GamesPlayed,-10} {team.GamesWon,-10} {team.GamesDrawn,-10} {team.GamesLost,-10} {team.GoalsFor,-10} {team.GoalsAgainst,-10} {team.GoalDifference,-10} {streakText,-10}</color>");
    }
}

}
