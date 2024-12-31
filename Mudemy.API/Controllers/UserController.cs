﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser()
        {
            await _userService.AddRoleToFirstUser();

            return Ok();
        }

        //api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var identity = HttpContext.User.Identity;
            if (identity == null || string.IsNullOrEmpty(identity.Name))
            {
                return BadRequest("User name is null or empty.");
            }
            var userName = identity.Name;
            return ActionResultInstance(await _userService.GetUserByNameAsync(userName));
        }
    }
}