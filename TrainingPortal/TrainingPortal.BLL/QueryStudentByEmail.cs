using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.BLL.Interfaces;
using  TrainingPortal.DAL;


namespace TrainingPortal.BLL
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
        
                //Checks if the input email exist within the database
                var isExist = _context.Students.Where(s => s.StudentEmail == EmailID).FirstOrDefault();

                return isExist != null;

            }
        }
}
