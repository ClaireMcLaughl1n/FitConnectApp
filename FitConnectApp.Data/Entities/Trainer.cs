using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitConnectApp.Data.Entities;

public class Trainer
{
    public int TrainerId { get; set; }

    [Required]
    public int UserId { get; set; } // Foreign key to the User entity

    [Required]
    public string Specialization { get; set; } // Specialization of the trainer (e.g., strength training, yoga, etc.)

    // Additional properties related to the trainer, such as experience, certifications, etc.
    
    // Navigation properties to establish relationships with other entities
    public User User { get; set; } // Reference to the associated User entity

    // Collection navigation property for Clients (one-to-many relationship)
    public ICollection<Client> Clients { get; set; } = new List<Client>();

    // Collection navigation property for TrainingSessions (one-to-many relationship)
    public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}