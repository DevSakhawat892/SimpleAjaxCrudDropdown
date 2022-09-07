using SimpleCrudDropdown.Domain.Models;
using SimpleCrudDropdown.Models;

namespace SimpleCrudDropdown.Infrastructur.Contracts
{
   public interface ICustomerRepository : IRepository<Customer>
   {
      public Task<Customer> GetCustomerByID(int key);

      public Task<Customer> GetCustomerByName(string customerName);

      public Task<IEnumerable<Customer>> GetCustomers();
   }
}
