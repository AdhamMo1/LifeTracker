using LifeTrackerEntities.DbSet;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LifeTrackerDataService.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<HealthData> HealthData {  get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
