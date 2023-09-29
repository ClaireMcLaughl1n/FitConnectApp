using Xunit;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using FitConnectApp.Data.Entities;
using FitConnectApp.Data.Services;
using Microsoft.EntityFrameworkCore;
using FitConnectApp.Data.Repositories;
using FitConnectApp.Data.Security;

namespace FitConnectApp.Test
{
    [Collection("Sequential")]
    public class CsvParserServiceCalculationTests
    {
        [Fact]
        public void CalculateAverageHeartRate_Should_Return_Average_HeartRate()
        {
            // Arrange
            var csvService = new CsvParserService();

            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1, // Set the userId
                    Type = "HKQuantityTypeIdentifierHeartRate",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 70
                },
                new HealthData
                {
                    UserId = 1, // Set the userId
                    Type = "HKQuantityTypeIdentifierHeartRate",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 80
                }
            };

            // Act
            var result = csvService.CalculateAverage(data, "HKQuantityTypeIdentifierHeartRate", DateTime.Parse("2023-09-01"), DateTime.Parse("2023-09-02"));

            // Assert
            Assert.Equal(75, result); // (70 + 80) / 2
        }

        [Fact]
        public void CalculateAverageSteps_Should_Return_Average_Steps()
        {
            // Arrange
            var csvService = new CsvParserService();
            
            // Create sample data for testing
            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1, // Set the userId
                    Type = "HKQuantityTypeIdentifierStepCount",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 10000
                },
                new HealthData
                {
                    UserId = 1, // Set the userId
                    Type = "HKQuantityTypeIdentifierStepCount",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 12000
                }
            };

            // Act
            var result = csvService.CalculateAverage(data, "HKQuantityTypeIdentifierStepCount", DateTime.Parse("2023-09-01"), DateTime.Parse("2023-09-02"));

            // Assert
            Assert.Equal(11000, result);
        }
        [Fact]
        public void CalculateAverageActiveCalories_Should_Return_Average_ActiveCalories()
        {
            // Arrange
            var csvService = new CsvParserService();
            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierActiveEnergyBurned",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 200
                },
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierActiveEnergyBurned",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 250
                }
            };
            var startDate = DateTime.Parse("2023-09-01");
            var endDate = DateTime.Parse("2023-09-02");

            // Act
            var result = csvService.CalculateAverage(data, "HKQuantityTypeIdentifierActiveEnergyBurned", startDate, endDate);

            // Assert
            Assert.Equal(225, result); // (200 + 250) / 2
        }

        [Fact]
        public void CalculateAverageRestingHeartRate_Should_Return_Average_RestingHeartRate()
        {
            // Arrange
            var csvService = new CsvParserService();
            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierRestingHeartRate",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 70
                },
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierRestingHeartRate",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 75
                }
            };
            var startDate = DateTime.Parse("2023-09-01");
            var endDate = DateTime.Parse("2023-09-02");

            // Act
            var result = csvService.CalculateAverage(data, "HKQuantityTypeIdentifierRestingHeartRate", startDate, endDate);

            // Assert
            Assert.Equal(72.5, result); // (70 + 75) / 2
        }

        [Fact]
        public void CalculateAverageWalkingHeartRate_Should_Return_Average_WalkingHeartRate()
        {
            // Arrange
            var csvService = new CsvParserService();
            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierWalkingHeartRateAverage",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 85
                },
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierWalkingHeartRateAverage",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 90
                }
            };
            var startDate = DateTime.Parse("2023-09-01");
            var endDate = DateTime.Parse("2023-09-02");

            // Act
            var result = csvService.CalculateAverage(data, "HKQuantityTypeIdentifierWalkingHeartRateAverage", startDate, endDate);

            // Assert
            Assert.Equal(87.5, result); // (85 + 90) / 2
        }

        [Fact]
        public void CalculateAverageSleep_Should_Return_Average_Sleep()
        {
            // Arrange
            var csvService = new CsvParserService();
            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1,
                    Type = "HKCategoryTypeIdentifierSleepAnalysis",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 7.5
                },
                new HealthData
                {
                    UserId = 1,
                    Type = "HKCategoryTypeIdentifierSleepAnalysis",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 8.0
                }
            };
            var startDate = DateTime.Parse("2023-09-01");
            var endDate = DateTime.Parse("2023-09-02");

            // Act
            var result = csvService.CalculateAverage(data, "HKCategoryTypeIdentifierSleepAnalysis", startDate, endDate);

            // Assert
            Assert.Equal(7.75, result); // (7.5 + 8.0) / 2
        }

        [Fact]
        public void CalculateAverageWaterIntake_Should_Return_Average_WaterIntake()
        {
            // Arrange
            var csvService = new CsvParserService();
            var data = new List<HealthData>
            {
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierDietaryWater",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 1500
                },
                new HealthData
                {
                    UserId = 1,
                    Type = "HKQuantityTypeIdentifierDietaryWater",
                    StartDate = DateTime.Parse("2023-09-01"),
                    EndDate = DateTime.Parse("2023-09-02"),
                    Value = 2000
                }
            };
            var startDate = DateTime.Parse("2023-09-01");
            var endDate = DateTime.Parse("2023-09-02");

            // Act
            var result = csvService.CalculateAverage(data, "HKQuantityTypeIdentifierDietaryWater", startDate, endDate);

            // Assert
            Assert.Equal(1750, result); // (1500 + 2000) / 2
        }
    }
}