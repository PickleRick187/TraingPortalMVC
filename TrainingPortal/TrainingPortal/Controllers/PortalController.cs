using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using TrainingPortal.DAL;
using TrainingPortal.DAL.Interfaces;
using TrainingPortal.BLL;
using TrainingPortal.BLL.Data;
using TrainingPortal.Models;


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




        // GET: Portal
        [Authorize]
        [HttpGet]
        public ActionResult UserHome(int id)
        {
            var StudentById = _studentRepository.GetStudentByID(id);
            if (StudentById != null)
            {
             return View("UserHome", StudentById);
            }

            return RedirectToAction("UserHome", "Home");
        }


        [ChildActionOnly]
        public PartialViewResult _CourseGallery()
        {
             ICollection<Course> courses = _courseRepository.GetCourses();

            return PartialView("_CourseGallery",courses);
        }


        public PartialViewResult Courses()
        {

            return PartialView();
        }


        //[HttpGet]
        //public async Task<ActionResult> _PersonalInfo(int? id)
        //{
        //    TrainingPortalEntities _db = new TrainingPortalEntities();

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Student useraccount = await _db.Students.FindAsync(id);
        //    if (useraccount == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(useraccount);
        //}

        [HttpGet]
        public ActionResult PersonalInfo(int id)
        {

            Student student = _studentRepository.GetStudentByID(id);

            return View("PersonalInfo", student);
        }

        [HttpPost]
        public ActionResult PersonalInfo(Student user)
        {
            TrainingPortalEntities _db = new TrainingPortalEntities();

            if (ModelState.IsValid)
            {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("UserHome", new { id = user.StudentID });
            }
            return View(user);
        }

        //[Authorize]
        //public ActionResult UserHome(int id)
        //{


        //    var model = studentRepository.GetStudentByID(id);
        //    if (model == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(model);
        //}
    }
}