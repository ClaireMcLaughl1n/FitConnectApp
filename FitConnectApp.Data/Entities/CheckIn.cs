using System.ComponentModel.DataAnnotations;

public class CheckIn
{
    public int CheckInId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }    
    public DateTime CheckInDate { get; set; }
    public double Weight { get; set; }
    public double Height {get; set; }
    public string PhotoUrl { get; set; }
    public string Notes { get; set; }
}
