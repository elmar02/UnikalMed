using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Abstract
{
    public interface ITokenService
    {
        Task<Token> CreateAccessToken(AppUser appUser, List<string> roles);
        string CreateRefreshToken();
    }
}
