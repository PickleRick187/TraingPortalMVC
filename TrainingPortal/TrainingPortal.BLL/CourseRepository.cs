//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TrainingPortal.BLL.Interfaces;
//using TrainingPortal.DAL;

//namespace TrainingPortal.BLL
//{
//    public class CourseRepository : ICourseRepository
//    {
//        private TrainingPortalEntities _context;


//        public CourseRepository(TrainingPortalEntities context)
//        {
//            _context = context;
//        }

//        public Course GetCourseByID(int id)
//        {
//            var getCourse = _context.Courses.Where(c => c.CourseID == id).FirstOrDefault();
//            return getCourse;
//        }

//        public ICollection<Course> GetCourses()
//        {
//            ICollection<Course> courses = _context.Courses.ToList();
//            return courses;
//        }

        
//    }
//}
