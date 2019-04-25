using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPortal.Models.Extended
{
    interface ICourseRepository : IDisposable
    {
        ICollection<Course> GetCourses();
        Course GetCourseByID(int CourseID);
        void InsertCourse(Course course);
        void DeleteCourse(int CourseID);
        void UpdateCourse(Course course);
        void Save();
    }
}
