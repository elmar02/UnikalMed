using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] role)
        {
            var result = await _authService.AssignRoleToUserAsnyc(userId, role);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string role)
        {
            var result = await _authService.RemoveRoleFromUserAsync(userId, role);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            if(result.StatusCode == HttpStatusCode.BadRequest)  
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> LogOut()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.LogOutAsync(userId);
            if(result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken)
        {
            var result = await _authService.RefreshTokenLoginAsync(refreshToken);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
