using AutoMapper;
using LifeTrackerDataService.Data;
using LifeTrackerDataService.IRepository;
using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Incoming;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.Repository
{
    public class HealthDataRepository : GenericRepository<HealthData> , IHealthDataRepository
    {
        private readonly IMapper _mapper;

        public HealthDataRepository(AppDbContext context, ILogger logger,IMapper mapper) : base(context, logger)
        {
            _mapper = mapper;
        }

        public HealthData GetHealthData(string IdentityId)
        {
           var HealthInfo = _context.HealthData.FirstOrDefault(x=>x.ApplicationUserId == IdentityId);
           return HealthInfo != null ? HealthInfo : null;
        }

        public async Task<HealthData> UpdateHealthData(string IdentityId, HealthDataInDto healthDataInDto)
        {
            var HealthInfo = _context.HealthData.FirstOrDefault(x => x.ApplicationUserId == IdentityId);
            if(HealthInfo == null)
            {
                var health = new HealthData { ApplicationUserId = IdentityId,
                    BloodType = healthDataInDto.BloodType,
                    Height = healthDataInDto.Height,
                    Weight = healthDataInDto.Weight,
                    Race = healthDataInDto.Race,
                    UseGlasses = healthDataInDto.UseGlasses,
                    AddedDate = DateTime.UtcNow.ToLocalTime(),
                };
                _context.HealthData.Add(health);
                await _context.SaveChangesAsync();
                return health;
            }
            HealthInfo.BloodType = healthDataInDto.BloodType;
            HealthInfo.Height = healthDataInDto.Height;
            HealthInfo.Weight = healthDataInDto.Weight;
            HealthInfo.Race = healthDataInDto.Race;
            HealthInfo.UseGlasses = healthDataInDto.UseGlasses;
            HealthInfo.UpdateDate = DateTime.UtcNow.ToLocalTime();
            await _context.SaveChangesAsync();
            return HealthInfo;
        }
    }
}
