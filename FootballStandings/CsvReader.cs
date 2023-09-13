using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

public class CsvReader<T>
{
    private readonly string filePath;

    public CsvReader(string filePath)
    {
        this.filePath = filePath;
    }

    public List<T> ReadCsv()
    {
        var records = new List<T>();

        try
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                records = csv.GetRecords<T>().ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV: {ex.Message}");
        }

        return records;
    }
}
