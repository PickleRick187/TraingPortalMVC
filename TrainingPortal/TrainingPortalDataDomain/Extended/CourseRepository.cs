using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models.Extended
{
    public class CourseRepository : ICourseRepository, IDisposable
    {
        private TrainingPortalEntities context;


        //public Course CourseCreatedDate(int number)
        //{
        //    List<Course> courses;
        //    if (number == 0)
        //    {
        //        courses = context.Courses.ToList();
        //    }
        //    else
        //    {

        //        //Ammend orderby CourseName to CreatedDate object
        //        courses = (from course in context.Courses
        //                   orderby course.CourseName descending
        //                   select course).Take(number).ToList();

        //    }
        //    return new Course();
        //}

        public CourseRepository(TrainingPortalEntities context)
        {
            this.context = context;
        }


        public ICollection<Course> GetCourses()
        {
            return context.Courses.ToList();
        }


        public Course GetCourseByID(int Id)
        {
            return context.Courses.Find(Id);
        }


        public void InsertCourse(Course course, HttpPostedFileBase image)
        {
           
            course.ImageMimeType = image.ContentType;
            course.PhotoFile = new byte[image.ContentLength];
            image.InputStream.Read(course.PhotoFile, 0, image.ContentLength);
            context.Courses.Add(course);
        }


        public void DeleteCourse(int CourseID)
        {
            Course course = context.Courses.Find(CourseID);
            context.Courses.Remove(course);
        }


        public void UpdateCourse(Course course)
        {
            context.Entry(course).State = EntityState.Modified;
        }


        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}