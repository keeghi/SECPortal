using SecPortal.Entities.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecPortal.Entities.Entities
{
    public class Project: BaseEntities
    {
        [Required, MaxLength(100)]
        public string ProjectName { set; get; }
        [MaxLength(255)]
        public string Description { set; get; }
        public int? HSuiteProjectId { set; get; }

        [ForeignKey("Organization")]
        
        public int OrganizationId { set; get; }
        public virtual Organization Organization { set; get; }

    }
}
