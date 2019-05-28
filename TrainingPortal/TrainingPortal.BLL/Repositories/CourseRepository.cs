using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPortal.DAL;
using TrainingPortal.BLL.Interfaces;

namespace TrainingPortal.BLL.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        
        public CourseRepository(TrainingPortalEntities context) : base(context)
        {

        }

        public IEnumerable<Course> GetTopSellingCourses(int count)
        {
            return TrainingPortalEntities.Courses.OrderByDescending(c => c.CoursePrice).Take(count).ToList();
        }

        public IEnumerable<Course> GetPopularCourses(int pageIndex, int pageSize)
        {
            return TrainingPortalEntities.Courses.OrderBy(c => c.CourseName).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

      

      

        public TrainingPortalEntities TrainingPortalEntities
        {
            get { return _context as TrainingPortalEntities; }
        }
 
    }
}
