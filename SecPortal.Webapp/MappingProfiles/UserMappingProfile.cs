using AutoMapper;
using SecPortal.Commons.ViewModels.UserViewModels;
using SecPortal.Entities.Entities;

namespace SecPortal.Webapp.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, GetUserResponse>()
                .ForMember(x => x.RoleName, d => d.MapFrom(dtl => dtl.Role.RoleName))
                .ForMember(x => x.OrganizationName, d => d.MapFrom(dtl => dtl.Organization != null ? dtl.Organization.OrganizationName : string.Empty))
                .ForMember(x => x.CreatedAtString, d => d.MapFrom(dtl => dtl.CreatedAt.ToString("dd-MM-yyyy hh:mm")))
                .ForMember(x => x.FullName, d => d.MapFrom(dtl => dtl.FirstName + " " + dtl.LastName));
        }
    }
}
