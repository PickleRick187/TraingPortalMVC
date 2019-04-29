using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainingPortal.Models;
using TrainingPortal.Models.Extended;

namespace TrainingPortal.Controllers
{
    public class PortalController : Controller
    {
        private IStudentRepository studentRepository;
        private ICourseRepository courseRepository;

        public PortalController()
        {
            this.studentRepository = new StudentRepository(new TrainingPortalEntities());
            this.courseRepository = new CourseRepository(new TrainingPortalEntities());
        }




        // GET: Portal
        [Authorize]
        [HttpGet]
        public ActionResult UserHome(int id)
        {
            var idStudentById = studentRepository.GetStudentByID(id);
        
                return View("UserHome", idStudentById);
                   
        }

        [ChildActionOnly]
        public PartialViewResult _CourseGallery()
        {
             ICollection<Course> courses = courseRepository.GetCourses();

            return PartialView("_CourseGallery",courses);
        }


        public PartialViewResult Courses()
        {

            return PartialView();
        }


        [HttpGet]
        public async Task<ActionResult> _PersonalInfo(int? id)
        {
            TrainingPortalEntities _db = new TrainingPortalEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student useraccount = await _db.Students.FindAsync(id);
            if (useraccount == null)
            {
                return HttpNotFound();
            }
            return View(useraccount);
        }

        [HttpGet]
        public ActionResult PersonalInfo(int id)
        {
            Student student = studentRepository.GetStudentByID(id);

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