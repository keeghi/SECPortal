using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecPortal.Commons.ViewModels.ProjectViewModels
{
    public class GetProjectResponse
    {
        public string ProjectName { set; get; }
        public string Description { set; get; }
        public int? HSuiteProjectId { set; get; }
    }
}
