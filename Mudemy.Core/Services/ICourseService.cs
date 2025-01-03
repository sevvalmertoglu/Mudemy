using Mudemy.Core.DTOs;
using SharedLibrary.Dtos;

namespace Mudemy.Core.Services
{
    public interface ICourseService
    {
        Task<Response<IEnumerable<CourseDto>>> GetAllCoursesAsync();

        Task<Response<CourseDto>> GetCourseByIdAsync(int id);

        Task<Response<CourseDto>> AddCourseAsync(CourseDto courseDto);

        Task<Response<CourseDto>> UpdateCourseAsync(int id, CourseDto courseDto);

        Task<Response<NoDataDto>> DeleteCourseAsync(int id);

        Task<Response<IEnumerable<CourseDto>>> GetCoursesByCategoryAsync(string category);
    }
}
