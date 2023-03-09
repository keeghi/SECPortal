using System;

namespace SecPortal.Commons.ViewModels.RoleViewModels
{
    public class GetRoleResponse
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool CanAccessAllOrganizations { get; set; }
        public string CreatedAtString { get; set; }

    }
}
