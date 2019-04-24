﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrainingPortal.Models.Extended
{
    public class CourseRepository : ICourseRepository, IDisposable
    {
        private TrainingPortalEntities context;


        public CourseRepository(TrainingPortalEntities context)
        {
            this.context = context;
        }


        public IEnumerable<Course> GetCourses()
        {
            return context.Courses.ToList();
        }


        public Course GetCourseByID(int Id)
        {
            return context.Courses.Find(Id);
        }


        public void InsertCourse(Course course)
        {
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