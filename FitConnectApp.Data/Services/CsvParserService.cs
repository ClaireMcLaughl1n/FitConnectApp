using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using FitConnectApp.Data.Entities; 
using CsvHelper;
using CsvHelper.Configuration;


public class CsvParserService 
{
    public List<HealthData> ParseCsv(string path, int userId)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Set HeaderValidated to null to ignore unmapped headers
            HeaderValidated = null
        });

        csv.Context.RegisterClassMap<HealthDataMap>();
        var healthDataList = csv.GetRecords<HealthData>().ToList();

        // Set userId for each healthdata record
        foreach (var healthData in healthDataList)
        {
            healthData.UserId = userId;
        }
        return healthDataList;
    }

    public double GetAverageHeartRate(string path, int userId, DateTime startDate, DateTime endDate)
    {
        var data = ParseCsv(path,userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKQuantityTypeIdentifierHeartRate", startDate, endDate);
    }

    public double GetAverageSteps(string path, int userId, DateTime startDate, DateTime endDate)
    {
        var data = ParseCsv(path, userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKQuantityTypeIdentifierStepCount", startDate, endDate);
    }

    public double GetAverageActiveCalories(string path, int userId, DateTime startDate, DateTime endDate)
    {
        var data = ParseCsv(path, userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKQuantityTypeIdentifierActiveEnergyBurned", startDate, endDate);
    }
    public double GetAverageRestingHeartRate(string path, int userId, DateTime startDate, DateTime endDate)
    {
        var data = ParseCsv(path, userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKQuantityTypeIdentifierRestingHeartRate", startDate, endDate);
    }
    public double GetAverageWalkingHeartRate(string path, int userId, DateTime startDate, DateTime endDate)
    {
        var data = ParseCsv(path, userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKQuantityTypeIdentifierWalkingHeartRateAverage", startDate, endDate);
    }

    public double GetAverageSleep(string path, int userId, DateTime startDate, DateTime endDate)
    {
        var data = ParseCsv(path, userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKCategoryTypeIdentifierSleepAnalysis", startDate, endDate);
    }

    public double GetWaterIntake(string path, int userId, DateTime startDate, DateTime endDate) 
    {
        var data = ParseCsv(path, userId);
        var userSpecificData = data.Where(d => d.UserId == userId && d.StartDate >= startDate && d.EndDate <= endDate);
        return CalculateAverage(data, "HKQuantityTypeIdentifierDietaryWater", startDate, endDate);
    }

    public double CalculateAverage(List<HealthData> data, string type, DateTime startDate, DateTime endDate)
    {
        var filteredData = data.Where(d => d.Type == type 
                                        && d.StartDate >= startDate 
                                        && d.EndDate <= endDate);

        double avgValue = 0; 

        if (filteredData.Any()) 
        {
            avgValue = filteredData.Average(d => d.Value);
        }

        return avgValue;
    }
}