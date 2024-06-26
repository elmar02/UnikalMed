﻿using Business.Abstract;
using Business.Messages;
using Business.Validation.AuthValidation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.ErrorResult;
using Core.Utilities.Results.Concrete.SuccessResult;
using Core.Utilities.Security.Abstract;
using Entities.Concrete;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;
        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<IResult> AssignRoleToUserAsnyc(string userId, string[] role)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            string responseMessage = string.Empty;
            if (user == null)
                return new ErrorResult(AuthStatus.UserNotFound, HttpStatusCode.Unauthorized);
            else
            {
                IdentityResult identityResult = await _userManager.AddToRolesAsync(user, role);
                if (identityResult.Succeeded)
                    return new SuccessResult(message: AuthStatus.RoleAddedSuccessfully, HttpStatusCode.OK);
                else
                {
                    foreach (var error in identityResult.Errors)
                        responseMessage += $"{error.Description}. ";
                    return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
                }
            }
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO)
        {
            var validator = new LoginUserValidation();
            var validationResult = validator.Validate(loginDTO);
            if (!validationResult.IsValid)
                return new ErrorDataResult<Token>(message: validationResult.ToString(), HttpStatusCode.BadRequest);

            var user = await _userManager.FindByNameAsync(loginDTO.EmailOrUsername);
            if (user == null)
                user = await _userManager.FindByEmailAsync(loginDTO.EmailOrUsername);

            if(user == null)
                return new ErrorDataResult<Token>(AuthStatus.UserNotFound, HttpStatusCode.Unauthorized);


            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, true);
            var roles = await _userManager.GetRolesAsync(user);

            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessToken(user, roles.ToList());
                var response = await UpdateRefreshToken(refreshToken: token.RefreshToken, user);
                return new SuccessDataResult<Token>(data: token, statusCode: HttpStatusCode.OK, message: response.Message);
            }
            else
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: AuthStatus.EmailOrPasswordWrong);
        }

        public async Task<IResult> LogOutAsync(string userId)
        {
            var findUser = await _userManager.FindByIdAsync(userId);
            if (findUser == null)
                return new ErrorResult(statusCode: HttpStatusCode.Unauthorized, message: AuthStatus.UserNotFound);
            findUser.RefreshToken = null;
            await _userManager.UpdateAsync(findUser);
            return new SuccessResult(statusCode: HttpStatusCode.OK, message: AuthStatus.LogoutSuccessfully);
        }

        public async Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            var roles = await _userManager.GetRolesAsync(user);

            if (user != null && user?.RefreshTokenExpiredDate > DateTime.UtcNow.AddHours(4))
            {
                Token token = await _tokenService.CreateAccessToken(user, roles.ToList());
                var response = await UpdateRefreshToken(refreshToken: refreshToken, user);
                if (response.Success)
                    return new SuccessDataResult<Token>(data: token, statusCode: HttpStatusCode.OK, message: response.Message);
                else
                    return new ErrorDataResult<Token>(statusCode: response.StatusCode, message: response.Message);
            }
            else
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.Unauthorized, message: AuthStatus.UserNotFound);
        }

        public async Task<IResult> RegisterAsync(RegisterDTO registerDTO)
        {
            try
            {
                var validator = new RegisterUserValidation();
                var validationResult = validator.Validate(registerDTO);
                if (!validationResult.IsValid)
                    return new ErrorResult(message: validationResult.ToString(), HttpStatusCode.BadRequest);

                var checkEmail = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == registerDTO.Email);
                var checkUserName = await _userManager.FindByNameAsync(registerDTO.Username);
                if (checkEmail != null)
                    return new ErrorResult(statusCode: HttpStatusCode.BadRequest, message: AuthStatus.EmailExists);

                if (checkUserName != null)
                    return new ErrorResult(statusCode: HttpStatusCode.BadRequest, message: AuthStatus.UsernameExists);

                AppUser newUser = new()
                {
                    Firstname = registerDTO.Firstname,
                    Lastname = registerDTO.Lastname,
                    Email = registerDTO.Email,
                    UserName = registerDTO.Username,
                };

                IdentityResult identityResult = await _userManager.CreateAsync(newUser, registerDTO.Password);
                string responseMessage = string.Empty;

                if (identityResult.Succeeded)
                {
                    responseMessage = AuthStatus.RegisteridSuccessfully;
                    return new SuccessResult(message: responseMessage, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                        responseMessage += $"{error.Description}. ";
                    return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult(message: ex.InnerException?.Message ?? MessageStatus.ExceptionMessage, HttpStatusCode.BadRequest);
            }
        }
        public async Task<IResult> RemoveRoleFromUserAsync(string userId, string role)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            string responseMessage = string.Empty;
            if (user == null)
                return new ErrorResult(AuthStatus.UserNotFound, HttpStatusCode.Unauthorized);
            else
            {
                var findRole = await _roleManager.FindByNameAsync(role);
                if (findRole == null)
                    return new ErrorResult(HttpStatusCode.BadRequest);

                IdentityResult identityResult = await _userManager.RemoveFromRoleAsync(user, role);
                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                        responseMessage += $"{error.Description}. ";
                    return new ErrorResult(message: responseMessage, HttpStatusCode.BadRequest);
                }
                else
                    return new SuccessResult(HttpStatusCode.OK);
            }
        }
        public async Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser user)
        {
            if (user is not null)
            {
                user.RefreshToken = refreshToken;

                user.RefreshTokenExpiredDate = DateTime.UtcNow.AddDays(1).AddHours(4);

                IdentityResult identityResult = await _userManager.UpdateAsync(user);
                string responseMessage = string.Empty;
                if (identityResult.Succeeded)
                    return new SuccessDataResult<string>(statusCode: HttpStatusCode.OK, data: refreshToken);
                else
                {
                    foreach (var error in identityResult.Errors)
                        responseMessage += $"{error.Description}. ";
                    return new ErrorDataResult<string>(message: responseMessage, HttpStatusCode.BadRequest);
                }
            }
            else
                return new ErrorDataResult<string>(AuthStatus.UserNotFound, HttpStatusCode.BadRequest);
        }
    }
}
