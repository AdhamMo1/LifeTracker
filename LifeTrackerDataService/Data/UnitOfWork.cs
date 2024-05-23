using AutoMapper;
using LifeTrackerDataService.IConfiguration;
using LifeTrackerDataService.IRepository;
using LifeTrackerDataService.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AppDbContext _context;
        private ILogger _logger;
        private IMapper _mapper;
        public UnitOfWork(AppDbContext context , ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("Db_logs");
            Users = new UserRepoository(context, _logger,_mapper);
            HealthData = new HealthDataRepository(context, _logger,_mapper);
        }
        public IUserRepository Users {  get; private set; }

        public IHealthDataRepository HealthData {  get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
