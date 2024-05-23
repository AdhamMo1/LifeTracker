using LifeTrackerEntities.DbSet;

namespace LifeTrackerDataService.IRepository
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
        Task<RefreshToken> CreateRefreshToken(ApplicationUser user);
    }
}
