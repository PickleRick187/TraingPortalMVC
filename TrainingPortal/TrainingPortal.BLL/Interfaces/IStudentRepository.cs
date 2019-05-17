using System;
using System.Collections.Generic;
using  TrainingPortal.BLL;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Interfaces
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
        Student VerifyEmail(string id);
        bool CheckReg(string EmailID);
    }
}
