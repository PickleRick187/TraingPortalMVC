using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TrainingPortal.Models
{
    public class CourseMetaData
    {

        [Key]
        public int CourseID { get; set; }


        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description of Course")]
        public string CourseDescription { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal CoursePrice { get; set; }


        public int CatalogID { get; set; }

        [MaxLength]
        public byte[] PhotoFile { get; set; }
    
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        //public virtual Catalog Catalog { get; set; }

    }
}