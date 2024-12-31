using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mudemy.Core.Models;
using Mudemy.Core.Repositories;

namespace Mudemy.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository

    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<Course>> GetPagedList(int pageSize, int pageCount)
        {
            throw new NotImplementedException();
        }
    }
}