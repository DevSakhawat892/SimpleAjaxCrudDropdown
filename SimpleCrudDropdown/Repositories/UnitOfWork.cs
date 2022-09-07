

using SimpleCrudDropdown.Contract;
using SimpleCrudDropdown.Contracts;
using SimpleCrudDropdown.DAL;

namespace SimpleCrudDropdown.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly DataContext context;
      public UnitOfWork(DataContext context)
      {
         this.context = context;
      }

      public void SaveChanges()
      {
         context.SaveChanges();
      }

      public void SaveChangesAsync()
      {
         context.SaveChangesAsync();
      }

      #region CustomerRepository
      private ICustomerRepository customerRepository;
      public ICustomerRepository CustomerRepository
      {
         get
         {
            if (customerRepository == null)
               customerRepository = new CustomerRepository(context);

            return customerRepository;
         }
      }
      #endregion
   }
}
