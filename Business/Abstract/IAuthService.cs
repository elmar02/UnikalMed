using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO registerDTO);
        Task<IResult> AssignRoleToUserAsnyc(string userId, string[] role);
        Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser user);
        Task<IResult> RemoveRoleFromUserAsync(string userId, string role);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO);
        Task<IResult> LogOutAsync(string userId);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken);
    }
}
