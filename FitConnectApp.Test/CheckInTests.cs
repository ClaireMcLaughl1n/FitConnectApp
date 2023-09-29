using Xunit;
using System;
using System.Linq;
using FitConnectApp.Data.Entities;
using FitConnectApp.Data.Services;
using Microsoft.EntityFrameworkCore;
using FitConnectApp.Data.Repositories;
using FitConnectApp.Data.Security;

namespace FitConnectApp.Test
{
    [Collection("Sequential")]
    public class CheckInTests
    {   
        private readonly ICheckInService _service;

        // public CheckInTests()
        // {
        //     // configure the data context options to use sqlite for testing
        //     var options = DatabaseContext.OptionsBuilder                            
        //                     .UseSqlite("Filename=test.db")
        //                     //.LogTo(Console.WriteLine)
        //                     .Options;
        //     var dbContext = new DatabaseContext(options);
        //     dbContext.Database.EnsureDeleted();
        //     dbContext.Database.EnsureCreated();
        //     _service = new CheckInDb(dbContext);
        // }

        [Fact]
        public void AddCheckIn_WhenCalled_ShouldAddCheckInToDatabase()
        {
            // Arrange
            var checkIn = new CheckIn { CheckInDate = DateTime.Now, Weight = 70, Notes = "Test Note" };

            // Act
            _service.AddCheckIn(checkIn);

            // Assert
            var result = _service.GetCheckInById(checkIn.CheckInId);
            Assert.NotNull(result);
            Assert.Equal(70, result.Weight);
        }

        [Fact]
        public void GetCheckInsByUserId_WhenCheckInsExistForUser_ShouldReturnCheckIns()
        {
            // Arrange
            var checkIn1 = new CheckIn { UserId = 1, CheckInDate = DateTime.Now, Weight = 70, Notes = "Test Note 1" };
            var checkIn2 = new CheckIn { UserId = 1, CheckInDate = DateTime.Now, Weight = 72, Notes = "Test Note 2" };

            // Act
            _service.AddCheckIn(checkIn1);
            _service.AddCheckIn(checkIn2);

            // Assert
            var results = _service.GetCheckInsByUserId(1).ToList();
            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void UpdateCheckIn_WhenCalled_ShouldUpdateCheckInInDatabase()
        {
            // Arrange
            var checkIn = new CheckIn { CheckInDate = DateTime.Now, Weight = 70, Notes = "Test Note" };
            _service.AddCheckIn(checkIn);

            // Act
            checkIn.Weight = 72;
            _service.UpdateCheckIn(checkIn);

            // Assert
            var updatedCheckIn = _service.GetCheckInById(checkIn.CheckInId);
            Assert.Equal(72, updatedCheckIn.Weight);
        }

        [Fact]
        public void DeleteCheckIn_WhenCalled_ShouldRemoveCheckInFromDatabase()
        {
            // Arrange
            var checkIn = new CheckIn { CheckInDate = DateTime.Now, Weight = 70, Notes = "Test Note" };
            _service.AddCheckIn(checkIn);

            // Act
            var resultBeforeDelete = _service.GetCheckInById(checkIn.CheckInId);
            Assert.NotNull(resultBeforeDelete);

            _service.DeleteCheckIn(checkIn.CheckInId);

            // Assert
            var resultAfterDelete = _service.GetCheckInById(checkIn.CheckInId);
            Assert.Null(resultAfterDelete);
        }
        [Fact]
        public void GetCheckIns_WhenNoCheckInsExist_ShouldReturnEmptyList()
        {
            // Arrange

            // Act
            var results = _service.GetCheckIns();

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void GetCheckInById_WhenCheckInDoesNotExist_ShouldReturnNull()
        {
            // Arrange

            // Act
            var result = _service.GetCheckInById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteCheckIn_WhenCheckInDoesNotExist_ShouldReturnFalse()
        {
            // Arrange

            // Act
            var result = _service.DeleteCheckIn(999);

            // Assert
            Assert.False(result);
        }
    }
}