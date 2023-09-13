using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

[TestClass]
public class CsvReaderTest
{
    [TestMethod]
    public void ReadTeamsCsv_ShouldReturnListOfTeams()
    {
        // Arrange
var teamCsvFilePath = @"C:\Users\kutuk\Football-standings-1\FootballStandings\Data\teams.csv";
        var csvReader = new CsvReader<Team>(teamCsvFilePath);

        // Act
        List<Team> teams = csvReader.ReadCsv();

        // Assert
        Assert.IsNotNull(teams);
        Assert.IsTrue(teams.Count > 0);
    }

    // Add more test methods for other CSV files and data models as needed
}
