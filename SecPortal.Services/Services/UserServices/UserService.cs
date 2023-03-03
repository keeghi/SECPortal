using AutoMapper;
using SecPortal.Entities.Data;
using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;

namespace SecPortal.Services.Services.UserServices
{
    public class UserService : CrudService<ApplicationUser>, IUserService
    {
        public UserService(IMapper mapper, IDataContext context) : base(mapper, context.Users)
        {

        }
    }
}
