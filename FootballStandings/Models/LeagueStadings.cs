using System;
using System.Collections.Generic;
using System.Linq;

public class LeagueStandings
{
    //Private field to store a list of Team objects.
    private List<Team> teams;

    //Constructor that initializes the class
    public LeagueStandings(List<Team> teams)
    {
        this.teams = teams;
    }

    
    public List<Team> CalculateStandings()
    {
        //Sort the teams by points, goal difference and goals 
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

        //Standings table.
        Console.WriteLine("League Standings:");
        Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10} {7,-10} {8,-10} {9,-10} {10,-10}",
                          "Pos", "Team", "Pts", "GP", "W", "D", "L", "GF", "GA", "GD", "Streak");

        //Iterate through the standings and print each team's information (as mentioned in the exercise description)
        foreach (var team in standings)
        {
            string specialMarking = "";

            //Add special rankings for teams in certain positions 
            //(CL = Chapions League, EL = Europe League, EC = Conference League)
            if (team.Position <= 1)
                specialMarking = "(CL)";
            else if (team.Position <= 3)
                specialMarking = "(EL)";
            else if (team.Position <= 6)
                specialMarking = "(EC)";

            string textColor = "white";

            //Change text color to red for teams in relegation zone
            if (team.Position >= teams.Count - 1)
                textColor = "red";

            //Print team information with formatting.
            Console.WriteLine($"<color={textColor}>{team.Position,-5} {specialMarking} {team.FullName,-25} {team.Points,-10} {team.GamesPlayed,-10} {team.GamesWon,-10} {team.GamesDrawn,-10} {team.GamesLost,-10} {team.GoalsFor,-10} {team.GoalsAgainst,-10} {team.GoalDifference,-10} {team.CurrentStreak,-10}</color>");
        }
    }
}
