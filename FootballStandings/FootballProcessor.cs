using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FootballProcessor
{
    private List<Team> originalTeams;
    private LeagueSetup leagueSetup;

    //Constructor for the FootballProcessor class
    public FootballProcessor(List<Team> teams, LeagueSetup leagueSetup)
    {
        this.originalTeams = teams;
        this.leagueSetup = leagueSetup;
        ResetTeamStatistics();
    }

    //Process the results of a round
    public void ProcessRoundResults(string roundFilePath)
    {
        List<MatchResult> matchResults = MatchResultProcessor.ReadMatchResults(roundFilePath);

        foreach (var matchResult in matchResults)
        {
            Team homeTeam = originalTeams.Find(team => team.Abbreviation == matchResult.HomeTeam);
            Team awayTeam = originalTeams.Find(team => team.Abbreviation == matchResult.AwayTeam);

            if (homeTeam != null && awayTeam != null)
            {
                //Update team statistics based on match results
                UpdateTeamStatistics(homeTeam, awayTeam, matchResult);
            }
        }

        CalculateStandings();
    }

    //Generate random scores for matches
    public void GenerateRandomScores(string roundFilePath)
    {
        List<string> lines = new List<string>();
        using (StreamReader reader = new StreamReader(roundFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }

        Random random = new Random();
        for (int i = 1; i < lines.Count; i++)
        {
            string[] parts = lines[i].Split(',');
            int homeGoals = random.Next(0, 5);
            int awayGoals = random.Next(0, 5);
            parts[2] = $"{homeGoals}-{awayGoals}";
            lines[i] = string.Join(",", parts);
        }

        using (StreamWriter writer = new StreamWriter(roundFilePath))
        {
            foreach (string line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }

    //Update team statistics based on results of a match
    private void UpdateTeamStatistics(Team homeTeam, Team awayTeam, MatchResult matchResult)
    {
        //Update game statistics
        homeTeam.GamesPlayed++;
        awayTeam.GamesPlayed++;
        homeTeam.GoalsFor += matchResult.HomeTeamGoals;
        homeTeam.GoalsAgainst += matchResult.AwayTeamGoals;
        awayTeam.GoalsFor += matchResult.AwayTeamGoals;
        awayTeam.GoalsAgainst += matchResult.HomeTeamGoals;

        UpdatePoints(homeTeam, awayTeam, matchResult);
        UpdateStreak(homeTeam, awayTeam, matchResult);
    }
    private void UpdatePoints(Team homeTeam, Team awayTeam, MatchResult matchResult)
    {
        //points for a win = 3, draw = 1, loss = 0
        if (matchResult.HomeTeamGoals > matchResult.AwayTeamGoals)
        {
            homeTeam.Points += 3;
            homeTeam.GamesWon++;
            awayTeam.GamesLost++;
        }
        else if (matchResult.HomeTeamGoals < matchResult.AwayTeamGoals)
        {
            awayTeam.Points += 3;
            awayTeam.GamesWon++;
            homeTeam.GamesLost++;
        }
        else
        {
            homeTeam.Points += 1;
            awayTeam.Points += 1;
            homeTeam.GamesDrawn++;
            awayTeam.GamesDrawn++;
        }
    }

    //Calculate and update league (table standings)
    private void CalculateStandings()
    {
        var orderedStandings = originalTeams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => team.GoalDifference)
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ToList();

        for (int i = 0; i < orderedStandings.Count; i++)
        {
            orderedStandings[i].Position = i + 1;
        }
    }

    //Update streaks for teams
    private void UpdateStreak(Team homeTeam, Team awayTeam, MatchResult matchResult)
    {
        bool homeTeamWon = matchResult.HomeTeamGoals > matchResult.AwayTeamGoals;
        bool awayTeamWon = matchResult.AwayTeamGoals > matchResult.HomeTeamGoals;
        bool isDraw = matchResult.HomeTeamGoals == matchResult.AwayTeamGoals;

        UpdateStreakForTeam(homeTeam, homeTeamWon, isDraw);
        UpdateStreakForTeam(awayTeam, awayTeamWon, isDraw);
    }

    //Update streaks for a team (wins, draws, losses)
    private void UpdateStreakForTeam(Team team, bool won, bool isDraw)
    {
        if (won)
        {
            team.CurrentStreak.Wins++;
            team.CurrentStreak.Draws = 0;
            team.CurrentStreak.Losses = 0;
        }
        else if (isDraw)
        {
            team.CurrentStreak.Draws++;
            team.CurrentStreak.Wins = 0;
            team.CurrentStreak.Losses = 0;
        }
        else
        {
            team.CurrentStreak.Losses++;
            team.CurrentStreak.Wins = 0;
            team.CurrentStreak.Draws = 0;
        }
    }

    //Display the current league standings
    public void DisplayCurrentStandings()
    {
        var orderedStandings = originalTeams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => team.GoalDifference)
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ToList();

        Console.WriteLine("League Standings:");
        Console.WriteLine("{0,-5} {1,-35} {2,-5} {3,-5} {4,-5} {5,-5} {6,-5} {7,-5} {8,-5} {9,-5} {10,-15}",
                          "Pos", "Team", "Pts", "GP", "W", "D", "L", "GF", "GA", "GD", "Streak");

        //Display each team's information in the standings table
        for (int i = 0; i < orderedStandings.Count; i++)
        {
            var team = orderedStandings[i];
            string specialMarking = GetSpecialMarking(i + 1);

            string teamName = $"{team.Position} {specialMarking} {team.FullName}";
            if (teamName.Length > 35)
            {
                teamName = teamName.Substring(0, 32) + "...";
            }

            string streakText = $"Wins: {team.CurrentStreak.Wins}, Draws: {team.CurrentStreak.Draws}, Losses: {team.CurrentStreak.Losses}";

            string textColor = GetTextColor(i + 1);
            Console.Write("\u001b[0m");
            Console.Write($"\u001b[{textColor}m");

            //Display team information in the standings table
            Console.WriteLine($"{team.Position,-5} {teamName,-35} {team.Points,-5} {team.GamesPlayed,-5} {team.GamesWon,-5} {team.GamesDrawn,-5} {team.GamesLost,-5} {team.GoalsFor,-5} {team.GoalsAgainst,-5} {team.GoalDifference,-5} {streakText,-15}");

            Console.Write("\u001b[0m");
        }
    }

    //Reset team statistics to initial values
    private void ResetTeamStatistics()
    {
        foreach (var team in originalTeams)
        {
            team.GamesPlayed = 0;
            team.GamesWon = 0;
            team.GamesDrawn = 0;
            team.GamesLost = 0;
            team.GoalsFor = 0;
            team.GoalsAgainst = 0;
            team.Points = 0;
            team.CurrentStreak.Wins = 0;
            team.CurrentStreak.Draws = 0;
            team.CurrentStreak.Losses = 0;
        }
    }

    //Text color for team names based on their position in the standings
    private string GetTextColor(int position)
    {
        if (position == 1)
            return "32"; // Green color for the top team
        if (position <= leagueSetup.PromoteToChampionsLeague)
            return "36";
        if (position >= originalTeams.Count - leagueSetup.RelegateToLowerLeague)
            return "31"; // Red color for teams at risk of relegation
        return "0"; // Default color
    }

    //Markings for teams based on their position in the league (CL, EL, EC)
    private string GetSpecialMarking(int position)
    {
        if (position <= leagueSetup.PromoteToChampionsLeague)
            return "(CL)"; //Champions League qualification
        if (position <= (leagueSetup.PromoteToChampionsLeague + leagueSetup.PromoteToEuropeLeague))
            return "(EL)"; //Europa League qualification
        if (position <= (leagueSetup.PromoteToChampionsLeague + leagueSetup.PromoteToEuropeLeague + leagueSetup.PromoteToConferenceLeague))
            return "(EC)"; //Conference League qualification
        return ""; //No special marking
    }
}
