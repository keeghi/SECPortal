using SecPortal.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecPortal.Entities.Repositories
{
    public interface IOrganizationRepository
    {
        IQueryable<Organization> Gets();
        Organization GetById(int id);
    }
}
