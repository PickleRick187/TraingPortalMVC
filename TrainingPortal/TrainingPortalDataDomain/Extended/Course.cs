using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    [MetadataType(typeof(CourseMetaData))]
    public partial class Course
    {

    }

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


        public byte[] PhotoFile { get; set; }


        public string ImageMimeType { get; set; }

        public virtual Catalog Catalog { get; set; }

    }
}