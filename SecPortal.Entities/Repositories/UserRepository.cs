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
    public class UserRepository : IUserRepository
    {
        protected IRepositoryFilter _repositoryFilter;
        private ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext, IRepositoryFilter repositoryFilter)
        {
            _repositoryFilter = repositoryFilter;
            _dbContext = dbContext;
        }

        public IQueryable<User> Gets()
        {
            IQueryable<User> result = _dbContext.Set<User>().AsQueryable();

            return result;
        }

        public User GetById(int id)
        {
            var result = Gets().Where(x => x.Id == id).SingleOrDefault();
            return result;
        }
    }
}
