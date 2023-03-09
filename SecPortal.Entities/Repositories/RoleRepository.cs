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
    public class RoleRepository : IRoleRepository
    {
        protected IRepositoryFilter _repositoryFilter;
        private ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext, IRepositoryFilter repositoryFilter)
        {
            _repositoryFilter = repositoryFilter;
            _dbContext = dbContext;
        }

        public IQueryable<Role> Gets()
        {
            IQueryable<Role> result = _dbContext.Set<Role>().AsQueryable();

            return result;
        }

        public Role GetById(int id)
        {
            var result = Gets().Where(x => x.Id == id).SingleOrDefault();
            return result;
        }
    }
}
