using AutoMapper;
using SecPortal.Entities.Entities;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using System.Collections.Generic;
using System.Linq;

namespace SecPortal.Services.Services.UserServices
{
    public class UserService : AutoMapperBase<User>, IUserService
    {
        private IUnitOfWork _dataContext;

        public UserService(IMapper mapper, IUnitOfWork context) : base(mapper)
        {
            _dataContext = context;
        }


        public List<User> GetUsers()
        {
            var result = _dataContext.Users.Gets();
            return result.ToList();
        }
    }

    public interface IUserService : IAutoMapperService<User>
    {
        List<User> GetUsers();
    }
}
