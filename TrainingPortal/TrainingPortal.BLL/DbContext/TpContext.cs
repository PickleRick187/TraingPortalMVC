using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL
{
    public  class TpContext : DbContext
    { 


        public TpContext() : base("name=TrainingPortalEntities")
        {

        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<TrainingType> TrainingTypes { get; set; }
    }
}
