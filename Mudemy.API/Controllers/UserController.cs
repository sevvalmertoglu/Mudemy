using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mudemy.Core.DTOs;
using Mudemy.Core.Services;

namespace Mudemy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("AddRoleToFirstUser")]
        public async Task<IActionResult> AddRoleToFirstUser()
        {
            await _userService.AddRoleToFirstUser();
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetUser()
        {
            var identity = HttpContext.User.Identity;
            var claims = HttpContext.User.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            if (identity == null || string.IsNullOrEmpty(identity.Name))
            {
                return BadRequest("User name is null or empty.");
            }
            var userName = identity.Name;
            return ActionResultInstance(await _userService.GetUserByNameAsync(userName));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto updateUserDto)
        {
            return ActionResultInstance(await _userService.UpdateUserProfileAsync(id, updateUserDto));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.Message);
            }

            return Ok("User deleted successfully.");
        }

    }
}