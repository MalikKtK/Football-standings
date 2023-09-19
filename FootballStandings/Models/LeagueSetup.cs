public class LeagueSetup
{
    public string LeagueName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public int NumberOfTeams { get; set; }


    public LeagueSetup(string leagueName, DateTime startDate, DateTime endDate, string location, int numberOfTeams)
    {
        LeagueName = leagueName;
        StartDate = startDate;
        EndDate = endDate;
        Location = location;
        NumberOfTeams = numberOfTeams;
    }

    public LeagueSetup()
    {
    }
}
