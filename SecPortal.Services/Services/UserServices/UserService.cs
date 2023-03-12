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

        public User GetUser(int id)
        {
            var result = _dataContext.Users.GetById(id);
            return result;
        }

        public User CreateUser(CreateUserRequest request)
        {
            var userModel = MapViewModelToModel<CreateUserRequest>(request);
            var result = _dataContext.Users.Create(userModel);
            return result;
        }

        public User UpdateUser(UpdateUserRequest request)
        {
            var entity = _dataContext.Users.GetById(request.Id);
            AssignValue(entity, request);
            _dataContext.Users.Update(entity);

            return entity;
        }
    }

    public interface IUserService : IAutoMapperService<User>
    {
        List<User> GetUsers();

        User GetUser(int id);

        User CreateUser(CreateUserRequest request);

        User UpdateUser(UpdateUserRequest request);
    }
}
