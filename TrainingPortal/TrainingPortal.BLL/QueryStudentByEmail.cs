using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.DAL.Interfaces;
using  TrainingPortal.DAL;


namespace TrainingPortal.Infrastructure.Data
{
    public class EfQueryStudentByEmail : IQueryStudentEmail
    {
        private TrainingPortalEntities _context;

        public EfQueryStudentByEmail(TrainingPortalEntities context)
        {
            this._context = context;
        }

        public bool IsEmailRegistered(string EmailID)
        {
            using (TrainingPortalEntities entities = new TrainingPortalEntities())
            {
                //Checks if the input email exist within the database
                var isExist = entities.Students.Where(s => s.StudentEmail == EmailID).FirstOrDefault();

                return isExist != null;

            }
        }
    }
}
