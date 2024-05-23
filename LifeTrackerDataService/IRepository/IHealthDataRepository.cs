using LifeTrackerEntities.DbSet;
using LifeTrackerEntities.Dto.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.IRepository
{
    public interface IHealthDataRepository : IGenericRepository<HealthData>
    {
        HealthData GetHealthData(string IdentityId);
        Task<HealthData> UpdateHealthData(string IdentityId,HealthDataInDto healthData);
    }
}
