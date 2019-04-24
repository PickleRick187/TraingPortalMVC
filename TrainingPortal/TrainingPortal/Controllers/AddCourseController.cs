using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainingPortal.Models;

namespace TrainingPortal.Controllers
{
    public class AddCourseController : Controller
    {
        private TrainingPortalEntities db = new TrainingPortalEntities();



        public async Task<ActionResult> Course()
        {
            return View(await db.Courses.ToListAsync());
        }



        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(Course courseDetail, HttpPostedFileBase image)
        {

            if (image == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return View("CreateCourse", courseDetail);
            }
            else
            {
                if (image != null)
                {
                    courseDetail.ImageMimeType = image.ContentType;
                    courseDetail.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(courseDetail.PhotoFile, 0, image.ContentLength);

                }
            }

            db.Courses.Add(courseDetail);
            db.SaveChanges();
            return View();
        }






        // GET: Admin/Delete/5
        public async Task<ActionResult> DeleteCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course courseDetail = await db.Courses.FindAsync(id);
            if (courseDetail == null)
            {
                return HttpNotFound();
            }
            return View(courseDetail);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCourseConfirmed(int id)
        {
            Course courseDetail = await db.Courses.FindAsync(id);
            db.Courses.Remove(courseDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Course");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var get = db.Courses.Find(id);

            return View(get);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Course course, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    course.ImageMimeType = image.ContentType;
                    course.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(course.PhotoFile, 0, image.ContentLength);

                }

                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CreateCourse");
            }
            return View(course);
        }


    }
}