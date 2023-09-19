using CsvHelper.Configuration;

public class TeamMap : ClassMap<Team>
{
    public TeamMap()
    {
        Map(m => m.Abbreviation).Name("Abbreviation");
        Map(m => m.FullName).Name("FullName");
        Map(m => m.SpecialRanking).Name("SpecialRanking");
        // Map other properties similarly
    }
}
