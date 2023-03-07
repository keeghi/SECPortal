using SecPortal.Entities.Data;
using SecPortal.Entities.Entities;
using SecPortal.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecPortal.Entities.Infrastructures
{
    public class UnitOfWork:IUnitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;
        private IOrganizationRepository _organizationRepository;


        public UnitOfWork(ApplicationDbContext dbContext, IOrganizationRepository organizationRepository )
        {
            _dbContext = dbContext;
            _organizationRepository = organizationRepository;
        }


        public IOrganizationRepository Organizations
        {
            get { return _organizationRepository; }
        }


        public void Commit()
            => _dbContext.SaveChanges();


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
