using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerEntities.DbSet
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsExpired { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpiryDate { get; set; }
        [ForeignKey("ApplicationUser")]
        public string  ApplicationUserId {  get; set; } 
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
