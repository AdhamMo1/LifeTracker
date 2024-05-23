using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerAuthentication.Dto.Incoming
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",ErrorMessage = "Password must be at least 8 characters, a combination of uppercase letters, lowercase letters, numbers, and symbols.")]
        public string Password { get; set; }
    }
}
