using System;

namespace SecPortal.Commons.ViewModels.UserViewModels
{
    public class GetUserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtString { get; set; }
    }
}
