using AutoMapper;
using SecPortal.Entities.Entities;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using System.Collections.Generic;
using System.Linq;

namespace SecPortal.Services.Services.UserServices
{
    public class OrganizationService : AutoMapperBase<Organization>
    {
        private IDataContext _dataContext;

        public OrganizationService(IMapper mapper, IDataContext context) : base(mapper)
        {
            _dataContext = context;
        }


        public List<Organization> GetOrganizations()
        {
            var result = _dataContext.Organizations.Gets();
            return result.ToList();
        }
    }
}
