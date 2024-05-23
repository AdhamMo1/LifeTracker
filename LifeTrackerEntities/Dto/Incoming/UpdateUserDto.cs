using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerEntities.Dto.Incoming
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
