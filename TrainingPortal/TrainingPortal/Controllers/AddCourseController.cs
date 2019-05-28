using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainingPortal.DAL;
using TrainingPortal.BLL.Interfaces;
using TrainingPortal.BLL;
using TrainingPortal.BLL.Repositories;
using TrainingPortal.Models;


namespace TrainingPortal.Controllers
{
    public class AddCourseController : Controller
    {
        private UnitOfWork _unitOfWork;
        public AddCourseController()
        {
            this._unitOfWork = new UnitOfWork(new TrainingPortalEntities());
        }


        public ActionResult Course()
        {
            var query = _unitOfWork.Courses.GetCollection();
            ICollection<CourseView> course = new List<CourseView>();
            var courseMap = AutoMapper.Mapper.Map(query, course);
            return View(courseMap);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course courseDetail, HttpPostedFileBase image)
        {

            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                var message = "Invalid ModelState";

                ViewBag.Message = message;

                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {

                    if (image != null)
                    {

                        //courseRepository.InsertCourse(courseDetail, image);
                        Course course = new Course
                        {
                            PhotoFile = new byte[image.ContentLength],
                            ImageMimeType = image.ContentType
                            
                        };
                        image.InputStream.Read(course.PhotoFile, 0, image.ContentLength);
                         _unitOfWork.Courses.Add(course);
                         _unitOfWork.Complete();
                        //await _unitOfWork.CompleteAsync();
                        return RedirectToAction("Course");
                    }

                }

            }
            return View(courseDetail);
        }





        // GET: Admin/Delete/5
        public ActionResult DeleteCourse(int id)
        {
            Course courseDetail = _unitOfWork.Courses.GetByID(id);

            if (courseDetail == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (courseDetail == null)
            {
                return HttpNotFound();
            }
            return View(courseDetail);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteCourseConfirmed(int id)
        {
            Course courseDetail = _unitOfWork.Courses.GetByID(id);
            _unitOfWork.Courses.Remove(courseDetail);
            _unitOfWork.Complete();
            return RedirectToAction("Course");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Course courseDetail = _unitOfWork.Courses.GetByID(id);

            return View(courseDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    course.ImageMimeType = image.ContentType;
                    course.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(course.PhotoFile, 0, image.ContentLength);

                }

              _unitOfWork.Courses.UpdatEntity(course);
              _unitOfWork.Complete();
                return RedirectToAction("Course");
            }
            return View(course);
        }


    }
}