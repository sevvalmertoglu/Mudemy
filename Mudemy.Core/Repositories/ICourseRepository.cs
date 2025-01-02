using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mudemy.Core.Models;
using Mudemy.Core.Repositories;

namespace Mudemy.Core.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<List<Course>> GetPagedList(int pageSize, int pageCount);
    }
}