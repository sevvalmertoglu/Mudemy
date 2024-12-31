using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mudemy.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommmitAsync();

        void Commit();
    }
}