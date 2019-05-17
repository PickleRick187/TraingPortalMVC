using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPortal.BLL.Interfaces
{
    public interface IQueryStudentEmail
    {
        bool IsEmailRegistered(string EmailID);

    }
}
