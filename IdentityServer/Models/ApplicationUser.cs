using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobilnr { get; set; }
        public int Role { get; set; }
        public int HospitalId { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }
}
