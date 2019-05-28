using Microsoft.Win32;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {        
        //IEnumerable<T> GetStudents<T>();
        bool CheckReg(string EmailID);
        Student GetEmail(string Email);
        Student VerifyEmail(string id);
        Student GetID(int id);

    }
}
