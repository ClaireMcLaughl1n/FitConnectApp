using System.ComponentModel.DataAnnotations;

public class CheckIn
{
    public int CheckInId { get; set; }
    public int ClientId { get; set; }
    public DateTime CheckInDate { get; set; }
    public double Weight { get; set; }
    public Measurements Measurements { get; set; }
    [Url]
    public string ProgressPhotos { get; set; }
    public FitnessMetrics FitnessMetrics { get; set; }
    [StringLength(500)]
    public string FitnessGoals { get; set; }
    [StringLength(500)]
    public string AdditionalNotes { get; set; }
}

public class Measurements
{
    public int ClientId { get; set; }
    public double Waist { get; set; }
    public double Hips { get; set; }
    public double Chest { get; set; }
    public double Arms { get; set; }
    public double Legs { get; set; }
}
