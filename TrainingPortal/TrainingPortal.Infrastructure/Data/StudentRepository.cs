using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPortal.Infrastructure.Data
{

 

    class StudentRepository : IStudentRepository, IDisposable
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
