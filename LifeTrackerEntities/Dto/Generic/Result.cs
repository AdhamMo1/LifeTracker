using LifeTrackerEntities.Dto.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerEntities.Dto.Generic
{
    public class Result<T> where T : class
    {
        public T Content { get; set; }
        public Error Error { get; set; }
        public bool Success => Error == null;
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}
