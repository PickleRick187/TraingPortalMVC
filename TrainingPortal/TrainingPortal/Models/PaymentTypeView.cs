using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models
{
    public class PaymentTypeView
    {
        public PaymentTypeView()
        {
            this.EnrollmentViews = new HashSet<EnrollmentView>();
        }

        public int TrainingTypeID { get; set; }
            public string TrainingTypeName { get; set; }

            [Display(Name = "Card Number")]
            public int CardNumber { get; set; }

            [Display(Name = "Card Holder Name")]
            public string CardHolderName { get; set; }

            [Display(Name = "Expiry Date")]
            public DateTime ExpiryDate { get; set; }

            [Display(Name = "Card Security Code")]
            public int CardSecurityCode { get; set; }


            public virtual ICollection<EnrollmentView> EnrollmentViews { get; set; }
    }
    }