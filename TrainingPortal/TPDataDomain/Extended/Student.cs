using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{   
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {
        public string ConfirmPassword { get; set; }

    }

    public class StudentMetaData
    {

        public StudentMetaData()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }

        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required.")]
        public string StudentFirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required.")]
        public string StudentLastName { get; set; }


        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        public string StudentEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required.")]
        public string StudentPassword { get; set; }

        [Compare("StudentPassword", ErrorMessage = "Confirm Password and Password does not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        public virtual ICollection<Enrollment> Enrollments { get; set; }


    }



}