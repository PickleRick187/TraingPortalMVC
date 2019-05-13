using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.Core.Interfaces;

namespace TrainingPortal.Core.Services
{
    class StudentService
    {
        private readonly IQueryStudentEmail _queryStudent;

        public StudentService(IQueryStudentEmail queryStudent)
        {
            this._queryStudent = queryStudent;
        }


        public bool IsEmailRegistered(string EmailID)
        {
            return _queryStudent.IsEmailRegistered(EmailID);
        }
    }
}
