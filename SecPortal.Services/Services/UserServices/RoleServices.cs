using AutoMapper;
using SecPortal.Entities.Entities;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using System.Collections.Generic;
using System.Linq;

namespace SecPortal.Services.Services.UserServices
{
    public class RoleService : AutoMapperBase<Role>, IRoleService
    {
        private IUnitOfWork _dataContext;

        public RoleService(IMapper mapper, IUnitOfWork context) : base(mapper)
        {
            _dataContext = context;
        }


        public List<Role> GetRoles()
        {
            var result = _dataContext.Roles.Gets();
            return result.ToList();
        }
    }

    public interface IRoleService : IAutoMapperService<Role>
    {
        List<Role> GetRoles();
    }
}
