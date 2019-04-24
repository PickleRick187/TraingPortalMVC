using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public PortalController()
        {
            this.studentRepository = new StudentRepository(new TrainingPortalEntities());

        }



        // GET: Portal
        [Authorize]
        [HttpGet]
        public ActionResult UserHome()
        {
            //var student = studentRepository.GetStudentByID(id);

            return View(/*student*/);
        }

        [ChildActionOnly]
        public ActionResult _CourseGallery()
        {
            using (TrainingPortalEntities entities = new TrainingPortalEntities())
            {
                List<Course> courses;
                courses = entities.Courses.ToList();

                return PartialView(courses);
            }
        }


        public ActionResult _UserName(int id)
        {
            TrainingPortalEntities _db = new TrainingPortalEntities();

            var query = from u in _db.Students
                        where u.StudentID == id
                        select u;
            Student user = (Student)query.FirstOrDefault();

            return PartialView("_UserName", user);
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


        [HttpPost]
        public ActionResult _PersonalInfo(Student user)
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

        public PartialViewResult _SideNav(Student student)
        {


            return PartialView(student);
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