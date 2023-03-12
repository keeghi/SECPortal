using System.ComponentModel.DataAnnotations;

namespace SecPortal.Commons.ViewModels.Account
{
    public class AccountIndexViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
        public string UserName { get; set; }
        public string LastLogin { get; set; }

        public AccountIndexViewModel()
        {

        }

        public AccountIndexViewModel(string userId, string email, string firstName, string lastName, string role)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}
