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

namespace Mudemy.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task AddRoleToFirstUser() // rol ataması yapar
        {
            var firstUser = _userManager.Users.FirstOrDefault();

            if (firstUser == null)
            {
                throw new InvalidOperationException("No users found in the database.");
            }

            await _roleManager.CreateAsync(new IdentityRole("instructor")); // tablolara ekler
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

            return Response<UserAppDto>.Success(200); // Silme işlemi başarılı.
        }

        public async Task<Response<UserAppDto>> UpdateUserProfileAsync(string id, CreateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Response<UserAppDto>.Fail("User not found", 404, true);
            }

            user.UserName = updateUserDto.UserName ?? user.UserName;
            user.Email = updateUserDto.Email ?? user.Email;
            
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, updateUserDto.Password);
            }

            await _userManager.UpdateAsync(user);
            return Response<UserAppDto>.Success(200);
        }

    }
}