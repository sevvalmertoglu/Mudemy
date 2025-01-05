using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mudemy.Core.DTOs;
using Mudemy.Core.Models;
using Mudemy.Core.Services;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mudemy.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;

            if (claimsPrincipal?.Identity == null || !claimsPrincipal.Identity.IsAuthenticated)
            {
                return Response<UserAppDto>.Fail("User not Auth.", 401, true);
            }


            var currentUserName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.GivenName)?.Value;

            if (string.IsNullOrWhiteSpace(currentUserName))
            {
                return Response<UserAppDto>.Fail("User Name is null.", 401, true);
            }

            var user = await _userManager.FindByNameAsync(currentUserName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task AddRoleToFirstUser() 
        {
            var firstUser = _userManager.Users.FirstOrDefault();

            if (firstUser == null)
            {
                throw new InvalidOperationException("No users found in the database.");
            }

            await _roleManager.CreateAsync(new IdentityRole("instructor"));
            await _roleManager.CreateAsync(new IdentityRole("user"));

            await _userManager.AddToRoleAsync(firstUser, "instructor");
            await _userManager.AddToRoleAsync(firstUser, "user");
        }

          public async Task<Response<UserAppDto>> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("User not found", 404, true);
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 500);
            }

            return Response<UserAppDto>.Success(200);
        }

        public async Task<Response<UserAppDto>> UpdateUserProfileAsync(string id, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Response<UserAppDto>.Fail("User not found", 404, true);
            }

            switch (updateUserDto.UpdateType)
            {
                case UpdateType.UserNameUpdate:
                    user.UserName = updateUserDto.UserName ?? user.UserName;
                    break;
                case UpdateType.EmailUpdate:
                    user.Email = updateUserDto.Email ?? user.Email;
                    break;
                case UpdateType.PasswordUpdate:
                    if (!string.IsNullOrEmpty(updateUserDto.CurrentPassword) &&
                        !string.IsNullOrEmpty(updateUserDto.NewPassword) &&
                        !string.IsNullOrEmpty(updateUserDto.ConfirmNewPassword))
                    {
                        var isPasswordValid = await _userManager.CheckPasswordAsync(user, updateUserDto.CurrentPassword);
                        if (!isPasswordValid)
                        {
                            return Response<UserAppDto>.Fail("Current password is incorrect", 400, true);
                        }

                        if (updateUserDto.NewPassword != updateUserDto.ConfirmNewPassword)
                        {
                            return Response<UserAppDto>.Fail("New passwords do not match", 400, true);
                        }

                        if (updateUserDto.CurrentPassword == updateUserDto.NewPassword)
                        {
                            return Response<UserAppDto>.Fail("New password cannot be the same as the current password", 400, true);
                        }

                        var result = await _userManager.ChangePasswordAsync(user, updateUserDto.CurrentPassword, updateUserDto.NewPassword);
                        if (!result.Succeeded)
                        {
                            var errors = result.Errors.Select(x => x.Description).ToList();
                            return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
                        }
                    }
                    break;
            }

            await _userManager.UpdateAsync(user);
            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 201);
        }


    }
}