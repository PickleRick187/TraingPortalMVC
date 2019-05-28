using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TrainingPortal.BLL;



namespace TrainingPortal.Models
{
   
    
    public class StudentLoginModel
        {
            [HiddenInput(DisplayValue = false)]
            public int StudentID { get; set; }

            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress)]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
            public string StudentEmail { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
            [MinLength(6, ErrorMessage = "Minimum 6 characters required.")]
            public string StudentPassword { get; set; }


            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
    }



    public  class RegisterModel
    {
        //eXPLICIT CASR DECLARATION FROM RegisterModel to Student
        //The cast should copy property data
        //public static explicit operator RegisterModel()
        //{
        //    return new RegisterModel() { name = this.name };
        //}

        [Key]
            [Required]
            public int StudentID { get; set; }

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

            [System.ComponentModel.DataAnnotations.Compare("StudentPassword", ErrorMessage = "Confirm Password and Password does not match.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }

            [HiddenInput(DisplayValue = false)]
            public System.Guid ActivationCode { get; set; }

            public bool IsEmailVerified { get; set; }

    }

    public class Portal
    {
        public int StudentID { get; set; }

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

    }


}




