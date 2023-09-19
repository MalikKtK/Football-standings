using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

[TestClass]
public class CsvReaderTests
{
    private const string FilePath = @"C:\Users\kutuk\Football standings\FootballStandings\Data\teams.csv";

[TestMethod]
public void ReadCsv_ShouldReturnListOfTeams()
{
    // Arrange
    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "teams.csv");
    var csvReader = new CsvReader<Team>(filePath);

    // Act
    List<Team> teams = csvReader.ReadCsv();

    // Assert
    Assert.IsNotNull(teams);
    Assert.IsTrue(teams.Count > 0);
}

}
