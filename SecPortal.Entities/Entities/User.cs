using SecPortal.Entities.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecPortal.Entities.Entities
{
    public class User:BaseEntities 
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(255)]
        public string Email { get; set; }
        [Required, MaxLength(255)]
        public string PasswordHash { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [ForeignKey("Organization")]
        public int? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public virtual ICollection<Role> CreatedByRoles { get; set; }
        public virtual ICollection<Role> ModifiedByRoles { get; set; }
        public virtual ICollection<Organization> CreatedByOrganizations { get; set; }
        public virtual ICollection<Organization> ModifiedByOrganizations { get; set; }
    }
}
