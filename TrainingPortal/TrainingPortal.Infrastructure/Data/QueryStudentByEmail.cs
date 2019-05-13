using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.


namespace TrainingPortal.Infrastructure.Data
{
    class EfQueryStudentByEmail : IQueryStudentEmail
    {
        public bool IsEmailRegistered(string EmailID)
        {
            var context = new TrainingPortalEntities();

            //Checks if the input email exist within the database
             var isExist = context.Students.Where(s => s.StudentEmail == EmailID).FirstOrDefault();

             return isExist != null;
        }
    }
}
