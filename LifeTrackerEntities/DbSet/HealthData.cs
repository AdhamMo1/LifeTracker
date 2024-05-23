using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerEntities.DbSet
{
    public class HealthData : BaseEntity
    {
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string? BloodType { get; set; }
        public string? Race { get; set; }
        public bool? UseGlasses { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public String ApplicationUserId { get; set; }
    }
}
