public class MatchResult
{
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public int HomeTeamGoals { get; set; }
    public int AwayTeamGoals { get; set; }
    public string MatchDate { get; set; }
    public string Stadium { get; set; }

    public MatchResult(string homeTeam, string awayTeam, int homeTeamGoals, int awayTeamGoals, string matchDate, string stadium)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        HomeTeamGoals = homeTeamGoals;
        AwayTeamGoals = awayTeamGoals;
        MatchDate = matchDate;
        Stadium = stadium;
    }
}
