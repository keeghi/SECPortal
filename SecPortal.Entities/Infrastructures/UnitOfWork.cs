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
        private IRoleRepository _roleRepository;


        public UnitOfWork(ApplicationDbContext dbContext, IOrganizationRepository organizationRepository,
            IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _dbContext = dbContext;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }


        public IOrganizationRepository Organizations
        {
            get { return _organizationRepository; }
        }


        public IUserRepository Users
        {
            get { return _userRepository; }
        }

        public IRoleRepository Roles
        {
            get { return _roleRepository; }
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
