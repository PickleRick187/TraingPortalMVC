using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    public class TrainingTypeView
    {
        public TrainingTypeView()
        {
            this.TrainingViews = new HashSet<TrainingView>();
        }

        public int TrainingTypeID { get; set; }
        public string TrainingTypeName { get; set; }

        public virtual ICollection<TrainingView> TrainingViews { get; set; }
    }
}