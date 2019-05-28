using System.Collections.Generic;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using TrainingPortal.DAL;
using TrainingPortal.BLL.Repositories;
using TrainingPortal.Models;
using TrainingPortal.BLL.Security;
using Crypto = TrainingPortal.BLL.Security.Crypto;


namespace TrainingPortal.Controllers
{
    public class PortalController : Controller
    {
        private UnitOfWork _unitOfWork;
        public PortalController()
        {
            this._unitOfWork = new UnitOfWork(new TrainingPortalEntities());
        }

        [HttpGet]
        [Authorize]
        public ActionResult UserHome(int id)
        {
            var queryStudent = _unitOfWork.Student.GetByID(id);
            if (queryStudent != null)
            {
            Portal portalStudent = new Portal();
            var auto = AutoMapper.Mapper.Map(queryStudent, portalStudent);

            return View(auto);
            }

            return RedirectToAction("SignIn", "Account");
        }

      

        [ChildActionOnly]
        public PartialViewResult _CourseGallery()
        {
            ICollection<Course> courses = _unitOfWork.Courses.GetCollection();
       
           ICollection<CourseView> courseView = new List<CourseView>();

           

            var course = AutoMapper.Mapper.Map(courses, courseView);
            return PartialView("_CourseGallery", course);
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
            var queryStudent = _unitOfWork.Student.GetByID(id);

            if (queryStudent != null)
            {
               Portal portalStudent = new Portal();
            var auto = AutoMapper.Mapper.Map(queryStudent, portalStudent);
            auto.StudentPassword = Crypto.Hash(queryStudent.StudentPassword);
                return View(auto);
            }
            return HttpNotFound();
        }


        //Edit: Student Details
        [HttpPost]
        public ActionResult PersonalInfo(Portal portalStudent)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student();

                var learner = AutoMapper.Mapper.Map(portalStudent, student);

             
  learner.StudentPassword = Crypto.Hash(portalStudent.StudentPassword);

                _unitOfWork.Student.UpdatEntity(learner);
              

                _unitOfWork.Complete();

                var autoPortal = AutoMapper.Mapper.Map(learner, portalStudent);

                return RedirectToAction("UserHome", new {id = autoPortal.StudentID});
            }
            return View(portalStudent);
        }
        #endregion

        [HttpGet]
        public ActionResult _CourseDetails(int id)
        {
            var queryCourse = _unitOfWork.Courses.GetByID(id);
            CourseView course = new CourseView();

            var courseMap = AutoMapper.Mapper.Map(queryCourse, course);


            return View(courseMap);
        }

       

    }

}