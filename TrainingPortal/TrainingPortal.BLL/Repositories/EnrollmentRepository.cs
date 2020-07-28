using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.BLL.Interfaces;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Repositories
{
    class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(TrainingPortalEntities context) : base(context)
        {
            
        }

        public TrainingPortalEntities TrainingPortalEntities
        {
            get { return _context as TrainingPortalEntities;  }
        }

    }
}
