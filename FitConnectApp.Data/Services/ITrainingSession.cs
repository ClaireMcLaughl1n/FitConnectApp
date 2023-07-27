using System;
using System.Collections.Generic;

namespace FitConnectApp.Data
{
    public interface ITrainingSession
    {
        // Get a single training session by its ID
        TrainingSession GetTrainingSessionById(int Id);

        // Get all training sessions
        IEnumerable<TrainingSession> GetAllTrainingSessions();

        // Add a new training session
        void AddTrainingSession(TrainingSession session);

        // Update an existing training session
        void UpdateTrainingSession(TrainingSession session);

        // Delete a training session by its ID
        void DeleteTrainingSession(int Id);
    }
}