using AutoMapper;
using SecPortal.Commons.ViewModels.ProjectViewModels;
using SecPortal.Entities.Entities;

namespace SecPortal.Webapp.MappingProfiles
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, GetProjectResponse>();
        }
    }
}
