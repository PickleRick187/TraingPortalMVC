using System;
using System.Linq;
using TrainingPortal.DAL;
using TrainingPortal.BLL.Interfaces;


namespace TrainingPortal.BLL.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {

        public StudentRepository(TrainingPortalEntities context) : base(context)
        {
            
        }

        public Student GetEmail(string Email)
        {
            var queryGetEmail = TrainingPortalEntities.Students.Where(s => s.StudentEmail == Email).FirstOrDefault();
            return queryGetEmail;
        }

        public Student GetID(int id)
        {
            var query = (from s in TrainingPortalEntities.Students
                where s.StudentID == id
                select s).FirstOrDefault();
            return query;
        }


        public bool CheckReg(string EmailID)
        {
            var check = TrainingPortalEntities.Students.Where(s => s.StudentEmail == EmailID).FirstOrDefault();
            return check != null;
        }

        //public IEnumerable<T> GetStudents<T>()
        //{
        //    var StudentList = TrainingPortalEntities.Students.ToList();

        //    return StudentList;
        //}


        public Student VerifyEmail(string id)
        {
            TrainingPortalEntities.Configuration.ValidateOnSaveEnabled = false;

            return TrainingPortalEntities.Students.Where(s => s.ActivationCode == new Guid(id)).FirstOrDefault();
        }


        public TrainingPortalEntities TrainingPortalEntities
        {
            get { return _context as TrainingPortalEntities; }
        }
    }
}
