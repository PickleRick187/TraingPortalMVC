using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    public class TrainingView
    {
        public TrainingView()
        {
            this.EnrollmentViews = new HashSet<EnrollmentView>();
        }

        public int TrainingID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int LecturerID { get; set; }
        public int TrainingTypeID { get; set; }

        public virtual ICollection<EnrollmentView> EnrollmentViews { get; set; }
        public virtual LecturerView LecturerView { get; set; }
        public virtual TrainingTypeView TrainingType { get; set; }
    }
}