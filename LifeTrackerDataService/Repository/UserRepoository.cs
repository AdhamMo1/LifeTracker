using AutoMapper;
using LifeTrackerDataService.Data;
using LifeTrackerDataService.IRepository;
using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Incoming;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.Repository
{
    public class UserRepoository : GenericRepository<ApplicationUser> , IUserRepository
    {
        public IMapper _mapper;
        public UserRepoository(AppDbContext context , ILogger logger ,IMapper mapper) : base(context,logger)
        {
            _mapper = mapper;
        }

        public override async Task<List<ApplicationUser>> All()
        {
            try
            {
                return await _context.Users.Where(x => x.Status == 1).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"{Repo} All Method has generated an error",typeof(UserRepoository));
                return new List<ApplicationUser>();
            }
        }

        public async Task<ApplicationUser> GetProfileByIdentity(string identityId)
        {
            var profile =  _context.Users.FirstOrDefault(x=>x.Status==1&&x.Id==identityId);
            if (profile == null)
                return null;
            return profile;
        }

        public async Task<ApplicationUser> UpdateProfileByIdentity(string identityId,UpdateUserDto UpdateUserDto)
        {
            var profile = _context.Users.FirstOrDefault(x => x.Status == 1 && x.Id == identityId);
            if (profile == null)
                return null;
            profile.FirstName = UpdateUserDto.FirstName??profile.FirstName;
            profile.LastName = UpdateUserDto.LastName??profile.LastName;
            profile.Country =  UpdateUserDto.Country ?? profile.Country;
            profile.Address = UpdateUserDto.Address ?? profile.Address;
            profile.DateOfBirth = UpdateUserDto.DateOfBirth ?? profile.DateOfBirth;
            profile.Sex = UpdateUserDto.Sex ?? profile.Sex;
            profile.PhoneNumber = UpdateUserDto.PhoneNumber ?? profile.PhoneNumber;
            profile.UpdateDate = UpdateUserDto.UpdateDate.ToLocalTime();
            _context.Users.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }
    }
}
