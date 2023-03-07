using SecPortal.Entities.Entities;
using SecPortal.Entities.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecPortal.Services.Services.UserServices
{
    public class OrganizationService
    {
        private IDataContext _dataContext;

        public OrganizationService(IDataContext context) { 
            _dataContext = context;
        }

        public List<Organization> GetOrganizations()
        {
            var result = _dataContext.Organizations.Gets();
            return result.ToList();
        }

    }
}
