using System.Collections.Generic;
using System.Threading.Tasks;


namespace TrainingPortal.BLL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(int id);
      

        IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> Find(Expreession<Func<TEntity, bool>> predicate);
        ICollection<TEntity> GetCollection();
        void Add(TEntity entity);
    
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void UpdatEntity(TEntity entity);
    }
}
