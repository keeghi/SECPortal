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
        private IUserRepository _userRepository;


        public UnitOfWork(ApplicationDbContext dbContext, IOrganizationRepository organizationRepository,
            IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }


        public IOrganizationRepository Organizations
        {
            get { return _organizationRepository; }
        }


        public IUserRepository Users
        {
            get { return _userRepository; }
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
