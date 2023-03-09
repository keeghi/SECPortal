using AutoMapper;
using SecPortal.Commons.ViewModels.UserViewModels;
using SecPortal.Entities.Entities;

namespace SecPortal.Webapp.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, GetUserResponse>();
        }
    }
}
