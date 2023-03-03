using System.ComponentModel.DataAnnotations;

namespace SecPortal.Common.ViewModel
{
    public class BaseViewModel
    {
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public BaseViewModel()
        {
            IsActive = true;
        }
    }
}
