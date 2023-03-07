using AutoMapper;
using SecPortal.Commons.ViewModels.OrganizationViewModels;
using SecPortal.Entities.Entities;

namespace SecPortal.Webapp.MappingProfiles
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Organization, GetOrganizationResponse>();
        }
    }
}
