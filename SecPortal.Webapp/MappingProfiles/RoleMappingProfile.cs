using AutoMapper;
using SecPortal.Commons.ViewModels.RoleViewModels;
using SecPortal.Entities.Entities;

namespace SecPortal.Webapp.MappingProfiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, GetRoleResponse>();
        }
    }
}
