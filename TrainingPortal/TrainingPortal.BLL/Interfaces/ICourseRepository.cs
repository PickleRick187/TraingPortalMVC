using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.BLL;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Interfaces
{
    public interface ICourseRepository
    {
        Course GetCourseByID(int id);
        ICollection<Course> GetCourses();
    }
}
