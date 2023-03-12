using SecPortal.Entities.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecPortal.Entities.Entities
{
    public class Organization : BaseEntities
    {
        [Required, MaxLength(100)]
        public string OrganizationName { set; get; }
        [MaxLength(255)]
        public string Description { set; get; }
        [MaxLength(255)]
        public string PrimaryContactEmailAddress { set; get; }
        public virtual List<Project> Projects { set; get; }
        }
}
