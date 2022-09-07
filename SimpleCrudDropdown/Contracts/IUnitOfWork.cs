using SimpleCrudDropdown.Contract;

namespace SimpleCrudDropdown.Contracts
{
   public interface IUnitOfWork
   {
      void SaveChanges();

      void SaveChangesAsync();

      ICustomerRepository CustomerRepository { get; }
   }
}
