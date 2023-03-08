using SecPortal.Commons.ViewModels.ProjectViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecPortal.Commons.ViewModels.OrganizationViewModels
{
    public class GetOrganizationResponse
    {
        [Required]
        public string OrganizationName { set; get; }
        public string Description { set; get; }
        public string PrimaryContactEmailAddress { set; get; }
        //public List<GetProjectResponse> Projects { set; get; }
    }
}
