using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mudemy.Core.UnitOfWork;
using Mudemy.Core.Repositories;
using Mudemy.Data.Repositories;

namespace Mudemy.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICourseRepository? _courseRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public ICourseRepository CourseRepository => _courseRepository ??= new CourseRepository(_context);


        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommmitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}