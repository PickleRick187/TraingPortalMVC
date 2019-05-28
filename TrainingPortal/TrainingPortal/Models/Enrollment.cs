using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    public partial class EnrollmentView
    {
        public int EnrollmentID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public int PaymentTypeID { get; set; }
        public int TrainingID { get; set; }

        public virtual CourseView CourseView { get; set; }
        public virtual PaymentTypeView PaymentTypeView { get; set; }
        public virtual Portal Portal { get; set; }
        public virtual TrainingView TrainingView { get; set; }
    }
}