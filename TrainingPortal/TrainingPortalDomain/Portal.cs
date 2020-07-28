using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPortalDomain
{
    public partial class StudentPortal
    { 
        public int StudentID { get; set; }

        public string StudentFirstName { get; set; }
            
        public string StudentLastName { get; set; }
        
        public string StudentEmail { get; set; }

        public string StudentPassword { get; set; }

        public bool IsEmailVerified { get; set; }

        public System.Guid ActivationCode { get; set; }
    }
}