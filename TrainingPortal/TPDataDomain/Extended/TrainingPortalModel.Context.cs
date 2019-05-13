using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace TrainingPortal.Models
{
    [MetadataType(typeof(TrainingPortalEntitiesMetaData))]
    public partial class TrainingPortalEntities : DbContext
    {

    }

    public class TrainingPortalEntitiesMetaData
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
    }
}