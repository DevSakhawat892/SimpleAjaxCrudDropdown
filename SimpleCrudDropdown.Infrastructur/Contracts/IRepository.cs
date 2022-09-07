using System.Linq.Expressions;

namespace SimpleCrudDropdown.Infrastructur.Contracts
{
   public interface IRepository<T>
   {
      T Add(T entity);

      T Get(int id);

      IEnumerable<T> GetAll();

      Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

      Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

      void update(T entity);

      void Delete(T entity);
   }
}
