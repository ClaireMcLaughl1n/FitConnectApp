using FitConnectApp.Data.Entities;

namespace FitConnectApp.Data.Services
{
    public static class Seeder
    {
        public static void Seed(IUserService userService, ICheckInService checkInService)
        {
            // Use the IUserService to seed users
            // SeedUsers(userService);
            // SeedTrainingSessions(trainingSession);

            // Use the ICheckInService to seed check-in data
            //SeedCheckIns(checkInService);
        }

        // private static void SeedUsers(IUserService svc)
        // {
        //     // svc.Initialise();

        //     svc.AddUser("Administrator", "admin@mail.com", "admin", Role.admin);
        //     svc.AddUser("Manager", "manager@mail.com", "manager", Role.manager);
        //     svc.AddUser("Client", "client@mail.com", "client", Role.client);
        // }

        // private static void SeedCheckIns(ICheckInService svc)
        // {
        //     // Create and add sample check-in data
        //     var checkIn1 = new CheckIn {Name = "John", Weight = 74, Height = 166, PhotoUrl="data:image/jpeg;base64,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", Notes ="Great week, feeling motivated"};
        //     svc.AddCheckIn(checkIn1);

        //     var checkIn2 = new CheckIn {Name="Jane", Weight = 55, Height = 154, PhotoUrl="https://images.squarespace-cdn.com/content/v1/55f4827ee4b094a4b11c8bde/1530476005369-ZHG2P6SR775E2M40R1FL/progress", Notes="2lbs down!"};
        //     svc.AddCheckIn(checkIn2);
        // }

        // private static void SeedTrainingSessions(ITrainingSession svc)
        // {
        //     // Combine date and time into a single DateTime object
        //     DateTime session1DateTime = DateTime.Today.AddDays(1).Add(TimeSpan.Parse("10:00:00")); // Adds 10 AM to the date
        //     DateTime session2DateTime = DateTime.Today.AddDays(2).Add(TimeSpan.Parse("10:00:00")); // Adds 10 AM to the date

        //     // Create and add sample session-data
        //     var session1 = new TrainingSession { 
        //         Date = session1DateTime, 
        //         Type = "Gym", 
        //         TrainerId = 1,
        //         UserId = 1 
        //     };
        //     svc.AddTrainingSession(session1);

        //     var session2 = new TrainingSession { 
        //         Date = session2DateTime, 
        //         Type = "Online", 
        //         TrainerId = 1,
        //         UserId = 1
        //     };
        //     svc.AddTrainingSession(session2);
        // }
    }
}