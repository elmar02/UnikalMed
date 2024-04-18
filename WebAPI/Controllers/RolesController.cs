using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody]string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] string roleName, [FromQuery] string roleId)
        {
            var result = await _roleService.UpdateRoleAsync(roleId, roleName);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRole([FromQuery] string id)
        {
            var response = await _roleService.DeleteRoleAsync(id);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRoles();
            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
