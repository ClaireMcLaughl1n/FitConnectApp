using System;
namespace FitConnectApp.Data.Entities
{
    public enum Role{ admin, user }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Height {get; set; }
        public Role Role {get; set; }
        public virtual ICollection<HealthData> HealthData {get; set; }
    }
}
