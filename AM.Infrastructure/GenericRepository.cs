using AM.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AM.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        readonly DbSet<T> dbSet;
        public GenericRepository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public void Delete(Expression<Func<T, bool>> where)
        {
            dbSet.RemoveRange(dbSet.Where(where));
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }
        public T GetById(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
