using SecPortal.Entities.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecPortal.Entities.Entities
{
    public class Role: BaseEntities
    {
        [Required, MaxLength(50)]
        public string RoleName { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public bool CanAccessAllOrganizations { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
