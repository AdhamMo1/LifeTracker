using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerAuthentication.Dto.Outgoing
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool isAuthanticated { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
