using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.IRepository
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
       Task<ApplicationUser> GetProfileByIdentity(string identityId);
       Task<ApplicationUser> UpdateProfileByIdentity(string identityId,UpdateUserDto userInDto);
    }
}
