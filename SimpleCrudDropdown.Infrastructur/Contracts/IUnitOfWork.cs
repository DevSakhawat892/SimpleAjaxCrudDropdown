namespace SimpleCrudDropdown.Infrastructur.Contracts
{
   public interface IUnitOfWork
   {
      void SaveChanges();

      ICustomerRepository CustomerRepository { get; }
   }
}
