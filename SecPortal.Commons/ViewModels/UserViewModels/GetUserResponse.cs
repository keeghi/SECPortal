using System;

namespace SecPortal.Commons.ViewModels.UserViewModels
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public string FullName { get; set; }

        public string CreatedAtString { get; set; }

    }
}
