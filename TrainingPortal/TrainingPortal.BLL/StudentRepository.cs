//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data.Entity;
//using  TrainingPortal.DAL;
//using TrainingPortal.BLL.Interfaces;


//namespace TrainingPortal.BLL
//{

//    public class StudentRepository : IStudentRepository, IDisposable
//    {

//        private TrainingPortalEntities _context;


//        public StudentRepository(TrainingPortalEntities context)
//        {
//            this._context = context;
//        }


//        public IEnumerable <T> GetStudents<T>()
//        {
//            return _context.Set<IEnumerable<>>.ToList();
//        }



//        public Student GetStudentByID(int studentID)
//        {
//            return _context.Students.Find(studentID);
//        }


       

//        public void InsertStudent(Student student)
//        {
//            _context.Students.Add(student);
//        }

//        public Student CheckEmail(string studentEmail)
//        {
//            return (from s in _context.Students
//                    where s.StudentEmail == studentEmail
//                select s).FirstOrDefault();
//        }


//        public Student VerifyEmail(string id)
//        {
//            _context.Configuration.ValidateOnSaveEnabled = false;
//            return _context.Students.Where(s => s.ActivationCode == new Guid(id)).FirstOrDefault();
//        }

//        public void DeleteStudent(int StudentID)
//        {
//            Student student = _context.Students.Find(StudentID);
//            _context.Students.Remove(student);
//        }


//        public void UpdateStudent(Student student)
//        {
//            _context.Entry(student).State = EntityState.Modified;
//        }


//        public void Save()
//        {
//            _context.SaveChanges();
//        }

//        public bool CheckReg(string EmailID)
//        {
//           var isExist = _context.Students.Where(e => e.StudentEmail == EmailID).FirstOrDefault();
//           return isExist != null;
//        }




//        private bool disposed = false;


//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    _context.Dispose();
//                }
//            }

//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//    }
//}
