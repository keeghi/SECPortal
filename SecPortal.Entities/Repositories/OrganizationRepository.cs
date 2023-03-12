using Microsoft.EntityFrameworkCore;
using SecPortal.Entities.Data;
using SecPortal.Entities.Entities;
using SecPortal.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecPortal.Entities.Repositories
{
    public class OrganizationRepository:IOrganizationRepository
    {
        private ApplicationDbContext _dbContext; 
        protected IRepositoryFilter _repositoryFilter;

        public OrganizationRepository(ApplicationDbContext dbContext, IRepositoryFilter repositoryFilter)
        {
            _dbContext = dbContext;
            _repositoryFilter = repositoryFilter;
        }

        public IQueryable<Organization> Gets()
        {
            IQueryable<Organization> result = null;

            if (_repositoryFilter.CurrentRole.CanAccessAllOrganizations)
            {
                result = _dbContext.Set<Organization>().AsQueryable();
            }
            else
            {
                var role = _repositoryFilter.CurrentRole;
                result = _dbContext.Set<OrganizationAccessFilter>()
                    .Where(x => x.RoleId == role.Id).Select(x => x.Organization).AsQueryable();
            }

            return result;
        }

        public Organization GetById(int id)
        {
            var result = Gets().Where(x => x.Id == id).SingleOrDefault();
            return result;
        }
    }
}
