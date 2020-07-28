using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrainingPortal.BLL.Interfaces;
using TrainingPortal.BLL.Security;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }


        public TEntity GetByID(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }


        //public IEnumerable<TEntity> Find(Expreession<Func<TEntity, bool>> predicate)
        //{
        //    _context.Set<TEntity>().Where(predicate);
        //}

        public ICollection<TEntity> GetCollection()
        {
            return _context.Set<TEntity>().ToList();
        }


        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }



        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }


        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }



        public void UpdatEntity(TEntity entity)
        {
          
            _context.Entry(entity).State = EntityState.Modified;
        }

     
    }
}









