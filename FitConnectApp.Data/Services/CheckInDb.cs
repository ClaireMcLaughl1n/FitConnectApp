using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FitConnectApp.Data.Entities;
using FitConnectApp.Data.Repositories;
namespace FitConnectApp.Data.Services
{
    public class CheckInDb : ICheckInService
    {
        private readonly DatabaseContext _dbContext;

        public CheckInDb(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<CheckIn> GetCheckIns()
        {
            return _dbContext.CheckIn.ToList();
        }

        public CheckIn GetCheckInById(int checkInId)
        {
            return _dbContext.CheckIn.FirstOrDefault(ci => ci.CheckInId == checkInId);
        }

        public IEnumerable<CheckIn> GetCheckInsByUserId(int userId)
        {
            return _dbContext.CheckIn.Where(ci => ci.UserId == userId).ToList();
        }

        public void AddCheckIn(CheckIn checkIn)
        {
            _dbContext.CheckIn.Add(checkIn);
            _dbContext.SaveChanges();
        }

        public void UpdateCheckIn(CheckIn checkIn)
        {
            var existingCheckIn = _dbContext.CheckIn
                .FirstOrDefault(ci => ci.CheckInId == checkIn.CheckInId);
            if (existingCheckIn != null)
            {
                existingCheckIn.CheckInDate = checkIn.CheckInDate;
                existingCheckIn.Weight = checkIn.Weight;
                existingCheckIn.Notes = checkIn.Notes;

                _dbContext.SaveChanges();
            }
        }

        public bool DeleteCheckIn(int checkInId)
        {
            var checkInToDelete = _dbContext.CheckIn.FirstOrDefault(ci => ci.CheckInId == checkInId);
            if (checkInToDelete != null)
            {
                _dbContext.CheckIn.Remove(checkInToDelete);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}