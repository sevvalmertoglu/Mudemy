using System.Collections.Generic;
using System.Threading.Tasks;
using Mudemy.Core.DTOs;
using Mudemy.Core.Models;
using Mudemy.Core.Repositories;
using Mudemy.Core.Services;
using SharedLibrary.Dtos;
using System.Linq;
using System.Linq.Expressions;
using Mudemy.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Mudemy.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseService(IGenericRepository<Course> courseRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<IEnumerable<CreateCourseDto>>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var courseDtos = ObjectMapper.Mapper.Map<IEnumerable<CreateCourseDto>>(courses);
            return Response<IEnumerable<CreateCourseDto>>.Success(courseDtos, 200);
        }

        public async Task<Response<CourseDto>> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                return Response<CourseDto>.Fail("Course not found", 404, true);

            var courseDto = ObjectMapper.Mapper.Map<CourseDto>(course);
            return Response<CourseDto>.Success(courseDto, 200);
        }

        public async Task<Response<CourseDto>> AddCourseAsync(CourseDto courseDto)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;

            if (claimsPrincipal?.Identity == null || !claimsPrincipal.Identity.IsAuthenticated)
            {
                return Response<CourseDto>.Fail("User not Auth.", 401, true);
            }

            var roles = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            if (roles != "Instructor")
            {
                return Response<CourseDto>.Fail("You are not authorized", 401, true);
            }

            var userId = claimsPrincipal?.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Response<CourseDto>.Fail("UserId not found in token", 401, true);
            }
            var course = ObjectMapper.Mapper.Map<Course>(courseDto);
            course.UserId = userId; 
            await _courseRepository.AddAsync(course);
            await _unitOfWork.CommmitAsync();
            var newCourseDto = ObjectMapper.Mapper.Map<CourseDto>(course);
            return Response<CourseDto>.Success(newCourseDto, 201);
        }

        public async Task<Response<CourseDto>> UpdateCourseAsync(int id, CourseDto courseDto)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null)
                return Response<CourseDto>.Fail("Course not found", 404, true);

            existingCourse.Name = courseDto.Name;
            existingCourse.Description = courseDto.Description;
            existingCourse.Price = courseDto.Price;
            existingCourse.Category = courseDto.Category;
            existingCourse.UpdatedAt = DateTime.UtcNow;

            _courseRepository.Update(existingCourse);
            await _unitOfWork.CommmitAsync();

            return Response<CourseDto>.Success(courseDto, 201);
        }

        public async Task<Response<NoDataDto>> DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                return Response<NoDataDto>.Fail("Course not found", 404, true);

            _courseRepository.Remove(course);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<CourseDto>>> GetCoursesByCategoryAsync(string category)
        {
            var courses = _courseRepository.Where(c => c.Category == category);
            var courseDtos = ObjectMapper.Mapper.Map<IEnumerable<CourseDto>>(await courses.ToListAsync());
            return Response<IEnumerable<CourseDto>>.Success(courseDtos, 200);
        }
    }
} 