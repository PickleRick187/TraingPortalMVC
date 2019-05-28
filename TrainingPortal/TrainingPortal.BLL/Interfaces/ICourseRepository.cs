using System.Collections.Generic;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetTopSellingCourses(int Count);
        IEnumerable<Course> GetPopularCourses(int pageIndex, int pageSize);
    }
}
