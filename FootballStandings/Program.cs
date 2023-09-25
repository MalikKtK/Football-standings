using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string dataDirectory = "Data";

        string setupFileName = "setup.csv";
        string setupFilePath = Path.Combine(dataDirectory, setupFileName);
        LeagueSetup leagueSetup = LeagueSetup.ParseSetupCsv(setupFilePath);

        if (leagueSetup == null)
        {
            Console.WriteLine("Unable to load league setup. Exiting...");
            return;
        }

        string teamFileName = "teams.csv";
        string teamFilePath = Path.Combine(dataDirectory, teamFileName);
        List<Team> teams = FootballCsvReader.ReadCsv(teamFilePath);

        if (teams == null)
        {
            Console.WriteLine("Unable to load team data. Exiting...");
            return;
        }

        FootballProcessor processor = new FootballProcessor(teams, leagueSetup);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Football Processor Menu:");
            Console.WriteLine("1. Print Teams");
            Console.WriteLine("2. Run Matches");
            Console.WriteLine("3. Display Current League Standings");
            Console.WriteLine("4. Simulate all matches");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        foreach (var team in teams)
                        {
                            Console.WriteLine($"Abbreviation: {team.Abbreviation}");
                            Console.WriteLine();
                        }
                        break;

                    case 2:
                        Console.Write("Enter the round file name (e.g., round-1.csv): ");
                        string roundFileName = Console.ReadLine();
                        string roundFilePath = Path.Combine(dataDirectory, roundFileName);

                        if (File.Exists(roundFilePath))
                        {
                            processor.GenerateRandomScores(roundFilePath);

                            processor.ProcessRoundResults(roundFilePath);

                            Console.WriteLine("Matches have been processed.");
                        }
                        else
                        {
                            Console.WriteLine("Round file not found. Please check the file name.");
                        }
                        break;

                    case 3:
                        processor.DisplayCurrentStandings();
                        break;

                    case 4:
                        Console.Write("Simulate all matches? (y/n): ");
                        string simulateAllMatches = Console.ReadLine();

                        if (simulateAllMatches.ToLower() == "y")
                        {
                            string[] roundFiles = Directory.GetFiles(dataDirectory, "round-*.csv");

                            if (roundFiles.Length == 0)
                            {
                                Console.WriteLine("No round files found in the 'Data' directory.");
                            }
                            else
                            {
                                Array.Sort(roundFiles);

                                foreach (string currentRoundFilePath in roundFiles)
                                {
                                    processor.GenerateRandomScores(currentRoundFilePath);

                                    processor.ProcessRoundResults(currentRoundFilePath);

                                    Console.WriteLine($"Matches in {Path.GetFileName(currentRoundFilePath)} have been processed.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Simulation canceled.");
                        }
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid menu choice.");
            }
        }
    }
}
