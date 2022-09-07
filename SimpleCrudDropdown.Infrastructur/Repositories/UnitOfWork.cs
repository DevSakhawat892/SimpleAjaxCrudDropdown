using SimpleCrudDropdown.Infrastructur.Contracts;
using SimpleCrudDropdown.Sql.DAL;

namespace SimpleCrudDropdown.Infrastructur.Repositories
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
