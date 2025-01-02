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

namespace Mudemy.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IGenericRepository<Course> courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<IEnumerable<CourseDto>>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            var courseDtos = ObjectMapper.Mapper.Map<IEnumerable<CourseDto>>(courses);
            return Response<IEnumerable<CourseDto>>.Success(courseDtos, 200);
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
            var course = ObjectMapper.Mapper.Map<Course>(courseDto);
            await _courseRepository.AddAsync(course);
            await _unitOfWork.CommmitAsync();
            var newCourseDto = ObjectMapper.Mapper.Map<CourseDto>(course);
            return Response<CourseDto>.Success(newCourseDto, 201);
        }

        public async Task<Response<NoDataDto>> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null)
                return Response<NoDataDto>.Fail("Course not found", 404, true);

            existingCourse.Name = updateCourseDto.Name;
            existingCourse.Description = updateCourseDto.Description;
            existingCourse.Price = updateCourseDto.Price;
            existingCourse.Category = updateCourseDto.Category;

            _courseRepository.Update(existingCourse);
            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(204);
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