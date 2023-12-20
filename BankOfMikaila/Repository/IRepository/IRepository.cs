using System.Linq.Expressions;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        T Get<TKey>(TKey id, params Expression<Func<T, object>>[] includeProperties);
        void Create(T entity);
        void Remove(T entity);
        bool Save();
    }
}
