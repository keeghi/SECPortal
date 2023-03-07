using SecPortal.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecPortal.Entities.Infrastructures
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();

        IOrganizationRepository Organizations { get; }
    }
}
