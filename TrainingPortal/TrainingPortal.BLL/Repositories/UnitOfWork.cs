using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.BLL.Interfaces;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Repositories
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrainingPortalEntities _context;

        public UnitOfWork(TrainingPortalEntities context)
        {
            _context = context;
            Student = new StudentRepository(_context);
            Courses = new CourseRepository(_context);
            Payment = new PaymentRepository(_context);
            Enrollment = new EnrollmentRepository(_context);
        }


        public ICourseRepository Courses { get; private set; }
        public IStudentRepository Student { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IEnrollmentRepository Enrollment { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
