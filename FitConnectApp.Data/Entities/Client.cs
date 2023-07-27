using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitConnectApp.Data.Entities;

public class Client
{
    public int ClientId { get; set; }

    [Required]
    public int UserId { get; set; } // Foreign key to the User entity

    [Required]
    public int TrainerId { get; set; } // Foreign key to the Trainer entity

    [Required]
    public string Goal { get; set; } // Fitness goal of the client (e.g., weight loss, muscle gain, etc.)

    // Additional properties related to the client, such as age, gender, etc.
    
    // Navigation properties to establish relationships with other entities
    public User User { get; set; } // Reference to the associated User entity
    //public Trainer Trainer { get; set; } // Reference to the associated Trainer entity

    // Collection navigation property for TrainingSessions (one-to-many relationship)
    public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

    // Collection navigation property for CheckIns (one-to-many relationship)
    public ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();

    // Collection navigation property for FitnessMetrics (one-to-many relationship)
    public ICollection<FitnessMetrics> FitnessMetrics { get; set; } = new List<FitnessMetrics>();
}

public class FitnessMetrics
{   [Key]
    public int ClientId {get; set; }
    public double BodyFatPercentage { get; set; }
    public double MuscleMassPercentage { get; set; }
    public string CardioFitnessLevel { get; set; }
}