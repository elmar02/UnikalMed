using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredDate { get; set; }
        virtual public string? PhotoUrl { get; set; }
    }
}
