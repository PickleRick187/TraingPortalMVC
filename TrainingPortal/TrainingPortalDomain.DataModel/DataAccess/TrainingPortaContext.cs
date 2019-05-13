using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingPortalDomain.DataModel.DataAccess
{
    [MetadataType(typeof(TrainingPortalEntitiesMetaData))]
    public partial class TrainingPortalEntities : DbContext
    {
    }

    public class TrainingPortalEntitiesMetaData
    {
        public DbSet<Student> Students { get; set; }

    }
}
