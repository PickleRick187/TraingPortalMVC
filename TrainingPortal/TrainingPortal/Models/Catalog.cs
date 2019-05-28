using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    public class CatalogView
    {

        public CatalogView()
        {
            this.Courses = new HashSet<CourseView>();
        }

        public int CatalogID { get; set; }
        public string CatalogName { get; set; }
        public string CatalogDescription { get; set; }


        public virtual ICollection<CourseView> Courses { get; set; }
    }
}