using System.Linq.Expressions;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        T Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        void Create(T entity);
        void Remove(T entity);
        bool Save();
    }
}
