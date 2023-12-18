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

        public T Get(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
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
