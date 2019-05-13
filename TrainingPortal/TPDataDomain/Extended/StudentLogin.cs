using System.ComponentModel.DataAnnotations;

namespace TrainingPortal.Models
{
    public class StudentLogin
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        public string StudentEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required.")]
        public string StudentPassword { get; set; }


        [Display(Name = "Remember Me")] public bool RememberMe { get; set; }
    }
}
