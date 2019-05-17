﻿using System.Web.Mvc;
using TrainingPortal.BLL;
using  TrainingPortal.DAL;

using TrainingPortal.BLL.Interfaces;


namespace TrainingPortal.Controllers
{
    public class HomeController : Controller
    {
        private ICourseRepository courseRepository;

        public HomeController()
        {
            this.courseRepository = new CourseRepository(new TrainingPortalEntities());
        }



         
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public PartialViewResult _CourseDetails(int id)
        {
            var model = courseRepository.GetCourseByID(id);
            return PartialView(model);
        }


        public FileContentResult GetImage(int id)
        {
            var image = courseRepository.GetCourseByID(id);
            if (image != null)
            {
                return File(image.PhotoFile, image.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        public PartialViewResult Courses()
        {

            return PartialView();
        }

    }



}