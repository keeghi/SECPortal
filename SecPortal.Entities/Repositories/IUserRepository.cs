using SecPortal.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecPortal.Entities.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Gets();
        User GetById(int id);
        User Create(User user);
        void Update(User user);
    }
}
