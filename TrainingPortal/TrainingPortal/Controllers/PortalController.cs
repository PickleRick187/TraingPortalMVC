using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using TrainingPortal.DAL;
using TrainingPortal.BLL;
using TrainingPortal.BLL.Interfaces;


namespace TrainingPortal.Controllers
{
    public class PortalController : Controller
    {

        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;

        public PortalController()
        {
            this._studentRepository = new StudentRepository(new TrainingPortalEntities());
            this._courseRepository = new CourseRepository(new TrainingPortalEntities());
        }


        // GET: Portal Page and current logged in student 
        [Authorize]
        [HttpGet]
        public ActionResult UserHome(int id)
        {
            var StudentById = _studentRepository.GetStudentByID(id);
            if (StudentById != null)
            {
                return View("UserHome", StudentById);
            }

            return RedirectToAction("UserHome", "Portal");
        }


        [ChildActionOnly]
        public PartialViewResult _CourseGallery()
        {
            ICollection<Course> courses = _courseRepository.GetCourses();

            return PartialView("_CourseGallery", courses);
        }


        public PartialViewResult Courses()
        {
            return PartialView();
        }

        #region Edit Student
        //Gets: Student Edit Page using the id to get current logged in student 
        [HttpGet]
        public ActionResult PersonalInfo(int id)
        {

            TrainingPortalEntities entities = new TrainingPortalEntities();

            var studentID = entities.Students.Find(id);

            return View("PersonalInfo", studentID);
        }


        //Edit: Student Details
        [HttpPost]
        public ActionResult PersonalInfo(Student user)
        {
            TrainingPortalEntities entities = new TrainingPortalEntities();

            if (ModelState.IsValid)
            {
                entities.Entry(user).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("UserHome", new {id = user.StudentID});
            }
            return View(user);
        }
        #endregion
    }
}