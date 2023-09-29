using CsvHelper.Configuration;
using FitConnectApp.Data.Entities;

public class HealthDataMap : ClassMap<HealthData>
{
    public HealthDataMap()
    {
        Map(m => m.Type).Name("type");
        Map(m => m.SourceName).Name("sourceName");
        Map(m => m.SourceVersion).Name("sourceVersion");
        Map(m => m.Unit).Name("unit");
        Map(m => m.CreationDate).Name("creationDate");
        Map(m => m.StartDate).Name("startDate");
        Map(m => m.EndDate).Name("endDate");
        Map(m => m.Value).Name("value");
    }
}