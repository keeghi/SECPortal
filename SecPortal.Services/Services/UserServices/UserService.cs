using AutoMapper;
using SecPortal.Commons.ViewModels.UserViewModels;
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

        public User CreateUser(CreateUserRequest request)
        {
            var userModel = MapViewModelToModel<CreateUserRequest>(request);
            var result = _dataContext.Users.Create(userModel);
            return result;
        }
    }

    public interface IUserService : IAutoMapperService<User>
    {
        List<User> GetUsers();

        User CreateUser(CreateUserRequest request);
    }
}
