using Microsoft.AspNetCore.Identity;

namespace LifeTrackerEntities.DbSet
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Status { get; set; } = 1;
        public DateTime? AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateDate { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
