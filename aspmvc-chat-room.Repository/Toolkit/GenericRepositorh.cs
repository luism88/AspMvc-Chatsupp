using System;
using System.Data.Entity;
using System.Linq;


namespace AspMvcChatsupp.DataAccess.Toolkit
{
    public abstract class GenericRepository<T> :
    IGenericRepository<T> where T : class 
    {

        private DbContext _entities = null;
       

        public GenericRepository(DbContext context)
        {
            _entities = context;
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
