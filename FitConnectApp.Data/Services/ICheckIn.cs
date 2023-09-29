using System;
using System.Collections.Generic;

namespace FitConnectApp.Data.Services
{
    public interface ICheckInService
    {
        // Get a single check-in by its ID
        CheckIn GetCheckInById(int checkInId);
        List<CheckIn> GetCheckIns();
        // Get all check-ins for a specific user
        IEnumerable<CheckIn> GetCheckInsByUserId(int userId);

        // Add a new check-in
        void AddCheckIn(CheckIn checkIn);
        // Update an existing check-in
        void UpdateCheckIn(CheckIn checkIn);

        // Delete a check-in by its ID
        bool DeleteCheckIn(int checkInId);
    }
}