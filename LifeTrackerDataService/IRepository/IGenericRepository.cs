using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDataService.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> All();
        Task<T> GetById(string id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id,string userId);
        // Update entity Or add if it deos not exist
        Task<bool> Upsert(T entity);
    }
}
