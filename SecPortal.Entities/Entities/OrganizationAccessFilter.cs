using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecPortal.Entities.Entities
{
    public class OrganizationAccessFilter
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        [ForeignKey("Organization")]
        public int OrganizationId { get; set;}
        public virtual Organization Organization{ get; set; }
    }
}
