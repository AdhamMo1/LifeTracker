using AutoMapper;
using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Incoming;
using LifeTrackerEntities.Dto.Outgoing;

namespace LifeTrackerAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserOutDto>();
            CreateMap<UserInDto, ApplicationUser>().
                ForMember(x => x.DateOfBirth, x => x.MapFrom(z => Convert.ToDateTime(z.DateOfBirth)));
            CreateMap<HealthData,HealthDataOutDto>();
            CreateMap<HealthDataInDto,HealthData>();
            
        }
    }
}
