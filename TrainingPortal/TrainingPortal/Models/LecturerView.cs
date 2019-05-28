using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    public class LecturerView
    {
        public LecturerView()
        {
            this.TrainingViews = new HashSet<TrainingView>();
        }

        public int LecturerID { get; set; }
        public string LecturerFirstName { get; set; }
        public string LecturerLastName { get; set; }
        public string LecturerEmail { get; set; }
        public string LecturerPassword { get; set; }

        public virtual ICollection<TrainingView> TrainingViews { get; set; }
    }
}