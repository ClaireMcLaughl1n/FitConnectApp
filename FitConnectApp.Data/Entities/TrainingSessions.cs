using System;
using System.ComponentModel.DataAnnotations;

public class TrainingSession
{
    public int TrainingSessionId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Time { get; set; }

    [Required]
    public string Type { get; set; } // In-person or online

    [Required]
    public int TrainerId { get; set; } // Foreign key to the Trainer entity

    [Required]
    public int ClientId { get; set; } // Foreign key to the Client entity

    // Additional properties related to the training session, such as location, duration, etc.

    // Navigation properties to establish relationships with other entities
    public Trainer Trainer { get; set; } // Reference to the associated Trainer entity
    public Client Client { get; set; } // Reference to the associated Client entity
}