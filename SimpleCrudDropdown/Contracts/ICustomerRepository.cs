using SimpleCrudDropdown.Contracts;
using SimpleCrudDropdown.Models;

namespace SimpleCrudDropdown.Contract
{
   public interface ICustomerRepository : IRepository<Customer>
   {
      public Task<Customer> GetCustomerByID(int key);

      public Task<Customer> GetCustomerByName(string customerName);

      public Task<IEnumerable<Customer>> GetCustomers();
   }
}
