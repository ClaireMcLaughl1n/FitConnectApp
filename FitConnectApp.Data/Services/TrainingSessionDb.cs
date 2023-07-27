using Microsoft.EntityFrameworkCore;
using FitConnectApp.Data.Entities;
using FitConnectApp.Data.Repositories;

namespace FitConnectApp.Data.Services;
    public class TrainingSessionDb : ITrainingSession
    {
        private readonly DatabaseContext _dbContext;

        public TrainingSessionDb(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TrainingSession GetTrainingSessionById(int sessionId)
        {
            return _dbContext.TrainingSessions.FirstOrDefault(session => session.TrainingSessionId == sessionId);
        }

        public IEnumerable<TrainingSession> GetAllTrainingSessions()
        {
            return _dbContext.TrainingSessions.ToList();
        }

        public void AddTrainingSession(TrainingSession session)
        {
            _dbContext.TrainingSessions.Add(session);
            _dbContext.SaveChanges();
        }

        public void UpdateTrainingSession(TrainingSession session)
        {
            var existingSession = _dbContext.TrainingSessions.FirstOrDefault(s => s.TrainingSessionId == session.TrainingSessionId);
            if (existingSession != null)
            {
                existingSession.Date = session.Date;
                existingSession.Time = session.Time;
                existingSession.Type = session.Type;
                existingSession.TrainerId = session.TrainerId;
                existingSession.ClientId = session.ClientId;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteTrainingSession(int sessionId)
        {
            var sessionToDelete = _dbContext.TrainingSessions.FirstOrDefault(session => session.TrainingSessionId == sessionId);
            if (sessionToDelete != null)
            {
                _dbContext.TrainingSessions.Remove(sessionToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
