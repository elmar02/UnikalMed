using Business.Abstract;
using Business.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleManager(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IResult> CreateRoleAsync(string roleName)
        {
            IdentityResult identityResult = await _roleManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName
            });

            if (identityResult.Succeeded)
                return new SuccessResult(message: AuthStatus.RoleAddedSuccessfully, HttpStatusCode.OK);
            else
            {
                string responseMessage = string.Empty;
                foreach (var error in identityResult.Errors)
                    responseMessage += $"{error.Description}. ";
                return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
            }
        }

        public async Task<IResult> DeleteRoleAsync(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            if (appRole == null)
                return new ErrorResult(statusCode: HttpStatusCode.Unauthorized, message: AuthStatus.RoleNotFound);

            IdentityResult identityResult = await _roleManager.DeleteAsync(appRole);
            if (identityResult.Succeeded)
                return new SuccessResult(message: AuthStatus.RoleRemovedSuccessfully, HttpStatusCode.OK);
            else
            {
                string responseMessage = string.Empty;
                foreach (var error in identityResult.Errors)
                    responseMessage += $"{error.Description}. ";
                return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<List<GetAllRolesDTO>>> GetAllRoles()
        {
            var result = _roleManager.Roles.ToList();
            var response = result.Select(x => new GetAllRolesDTO
            {
                Id = x.Id,
                RoleName = x.Name
            }).ToList();
            return new SuccessDataResult<List<GetAllRolesDTO>>(data: response, statusCode: HttpStatusCode.OK);
        }

        public async Task<IResult> UpdateRoleAsync(string roleId, string roleName)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(roleId);
            if (appRole == null)
                return new ErrorResult(statusCode: HttpStatusCode.Unauthorized, message: AuthStatus.RoleNotFound);
            appRole.Name = roleName;
            IdentityResult identityResult = await _roleManager.UpdateAsync(appRole);
            if (identityResult.Succeeded)
                return new SuccessResult(message: AuthStatus.RoleUpdatedSuccessfully, HttpStatusCode.OK);
            else
            {
                string responseMessage = string.Empty;
                foreach (var error in identityResult.Errors)
                    responseMessage += $"{error.Description}. ";
                return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
            }
        }
    }
}
