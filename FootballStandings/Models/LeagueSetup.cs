using System;
using System.IO;

public class LeagueSetup
{
    //Properties representing league setup
    public string LeagueName { get; set; }
    public int PromoteToChampionsLeague { get; set; }
    public int PromoteToEuropeLeague { get; set; }
    public int PromoteToConferenceLeague { get; set; }
    public int PromoteToUpperLeague { get; set; }
    public int RelegateToLowerLeague { get; set; }

    //Constructor to initialize league setup
    public LeagueSetup(string leagueName, int promoteToChampionsLeague, int promoteToEuropeLeague,
                       int promoteToConferenceLeague, int promoteToUpperLeague, int relegateToLowerLeague)
    {
        LeagueName = leagueName;
        PromoteToChampionsLeague = promoteToChampionsLeague;
        PromoteToEuropeLeague = promoteToEuropeLeague;
        PromoteToConferenceLeague = promoteToConferenceLeague;
        PromoteToUpperLeague = promoteToUpperLeague;
        RelegateToLowerLeague = relegateToLowerLeague;
    }

    //Method to parse a setup.csv file and create the object "LeagueSetup"
    public static LeagueSetup ParseSetupCsv(string filePath)
    {
        try
        {
            //Read all lines from the CSV file
            string[] lines = File.ReadAllLines(filePath);

        
            if (lines.Length < 6)
            {
                throw new Exception("Invalid setup.csv format. Missing data.");
            }

            
            return new LeagueSetup(
                lines[0],
                int.Parse(lines[1]),
                int.Parse(lines[2]),
                int.Parse(lines[3]),
                int.Parse(lines[4]),
                int.Parse(lines[5])
            );
        }
        catch (Exception ex)
        {
            //Handle any exceptions and print an error message
            Console.WriteLine($"Error parsing setup.csv: {ex.Message}");
            return null;
        }
    }
}
