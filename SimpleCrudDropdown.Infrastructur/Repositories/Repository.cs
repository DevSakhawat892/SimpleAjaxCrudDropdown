using Microsoft.EntityFrameworkCore;
using SimpleCrudDropdown.Infrastructur.Contracts;
using SimpleCrudDropdown.Sql.DAL;
using System.Linq.Expressions;

namespace SimpleCrudDropdown.Infrastructur.Repositories
{
   public class Repository<T> : IRepository<T> where T : class
   {
      private readonly DataContext context;
      public Repository(DataContext context)
      {
         this.context = context;
      }

      public virtual T Add(T entity)
      {
         try
         {
            return context.Set<T>().Add(entity).Entity;
         }
         catch (Exception)
         {

            throw;
         }
      }

      public virtual T Get(int id)
      {
         try
         {
            return context.Set<T>().Find(id);
         }
         catch (Exception)
         {

            throw;
         }
      }

      public IEnumerable<T> GetAll()
      {
         try
         {
            return context.Set<T>().AsQueryable().AsNoTracking().ToList();
         }
         catch (Exception)
         {

            throw;
         }
      }

      public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return await context.Set<T>().AsQueryable().AsNoTracking().Where(predicate).ToListAsync();
         }
         catch (Exception)
         {

            throw;
         }
      }

      public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
      {
         try
         {
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
         }
         catch (Exception)
         {

            throw;
         }
      }

      public virtual void update(T entity)
      {
         try
         {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<T>().Update(entity);
         }
         catch (Exception)
         {

            throw;
         }
      }

      public virtual void Delete(T entity)
      {
         try
         {
            context.Set<T>().Remove(entity);
         }
         catch (Exception)
         {

            throw;
         }
      }
   }
}
