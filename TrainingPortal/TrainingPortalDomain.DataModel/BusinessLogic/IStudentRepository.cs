using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrainingPortalDomain.DataModel.DataAccess;

namespace TrainingPortalDomain.DataModel.BusinessLogic
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentByID(int studentID);
        Student CheckEmail(string studentEmail);
        void InsertStudent(Student student);
        void DeleteStudent(int studentID);
        void UpdateStudent(Student student);
        void Save();
        bool IsEmailExist(string emailID);
    }

    public class StudentRepository : IStudentRepository, IDisposable
    {

        private TrainingPortalEntities context;


        public StudentRepository(TrainingPortalEntities _context)
        {
            this.context = _context;
        }


        public IEnumerable<Student> GetStudents()
        {
            return context.Students.ToList();
        }

  

        public Student GetStudentByID(int Id)
        {
            return context.Students.Find(Id);
        }


        public void InsertStudent(Student student)
        { 
          context.Students.Add(student);
        }

        public Student CheckEmail(string studentEmail)
        {
            var v = (from s in context.Students
                where s.StudentEmail == studentEmail
                select s).FirstOrDefault();

            return v;
        }

        public void DeleteStudent(int StudentID)
        {
            Student student = context.Students.Find(StudentID);
            context.Students.Remove(student);
        }


        public void UpdateStudent(Student student)
        {
            context.Entry(student).State = EntityState.Modified;
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




        public bool IsEmailExist(string emailID)
        {


            var existEmail = context.Students.Where(s => s.StudentEmail == emailID).FirstOrDefault();

                return existEmail != null;
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}