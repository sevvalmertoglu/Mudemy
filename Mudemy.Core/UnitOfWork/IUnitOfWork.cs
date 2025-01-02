using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mudemy.Core.Repositories;

namespace Mudemy.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        Task CommmitAsync();

        void Commit();
    }
}