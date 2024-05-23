using LifeTrackerDataService.Data;
using LifeTrackerDataService.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context { get; set; }
        protected ILogger _logger { get; set; }
        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public virtual async Task<bool> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return true;
        }

        public virtual async Task<List<T>> All()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual Task<bool> Delete(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
