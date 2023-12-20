using BankOfMikaila.Data;
using BankOfMikaila.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankOfMikaila.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);
            if (!Save())
            {
                Console.WriteLine("Nothing was saved during creation...");
            }
        }

        public T Get<TKey>(TKey id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(e => EF.Property<TKey>(e, "Id").Equals(id));
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            if (!Save())
            {
                Console.WriteLine("Nothing was saved during deletion...");
            }
        }

        public bool Save()
        {
           return _db.SaveChanges() > 0; //returns the number of entries saved to the database. if greater than 0, something was saved.
        }
    }
}
