using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mudemy.Core.DTOs;
using Mudemy.Core.Models;
using Mudemy.Core.Services;

namespace Mudemy.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly IServiceGeneric<Course, CourseDto> _courseService;

        public CourseController(IServiceGeneric<Course, CourseDto> courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var userClaims = User.Claims;

            return ActionResultInstance(await _courseService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SaveCourse(CourseDto courseDto)
        {
            return ActionResultInstance(await _courseService.AddAsync(courseDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(CourseDto courseDto)
        {
            return ActionResultInstance(await _courseService.Update(courseDto, courseDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            return ActionResultInstance(await _courseService.Remove(id));
        }
    }
}