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
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return ActionResultInstance(await _courseService.GetAllCoursesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            return ActionResultInstance(await _courseService.GetCourseByIdAsync(id));
        }

        [Authorize(Policy = "InstructorRole")]
        [HttpPost]
        public async Task<IActionResult> SaveCourse(CourseDto courseDto)
        {
            return ActionResultInstance(await _courseService.AddCourseAsync(courseDto));
        }

        [Authorize(Policy = "InstructorRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            return ActionResultInstance(await _courseService.UpdateCourseAsync(id, updateCourseDto));
        }

        [Authorize(Policy = "InstructorRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            return ActionResultInstance(await _courseService.DeleteCourseAsync(id));
        }
    }

}