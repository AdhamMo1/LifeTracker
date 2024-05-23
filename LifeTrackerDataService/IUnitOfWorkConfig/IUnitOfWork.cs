using LifeTrackerDataService.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IHealthDataRepository HealthData { get; }
       
        Task CompleteAsync();
    }
}
