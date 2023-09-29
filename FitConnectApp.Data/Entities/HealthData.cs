using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using FitConnectApp.Data.Entities;

public class HealthData
{
    public int Id {get; set; }
    [Name("type")]
    public string Type { get; set; }
    [Name("sourceName")]
    public string SourceName { get; set; }
    [Name("sourceVersion")]
    public string SourceVersion { get; set; }
    [Name("unit")]
    public string Unit { get; set; }
    [Name("creationDate")]
    public DateTime CreationDate { get; set; }
    [Name("startDate")]
    public DateTime StartDate { get; set; }
    [Name("endDate")]
    public DateTime EndDate { get; set; }
    [Name("value")]
    public double Value { get; set; }
    public int UserId {get; set; }
    public User User {get; set; }
}

public class HealthDataParser
{
    public List<HealthData> ParseCsvWithCsvHelper(string path, int userId)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var healthDataList = csv.GetRecords<HealthData>().ToList();

        //set userId for each healthdata record
        foreach (var healthData in healthDataList)
        {
            healthData.UserId = userId;
        }
        return healthDataList;
    }
}