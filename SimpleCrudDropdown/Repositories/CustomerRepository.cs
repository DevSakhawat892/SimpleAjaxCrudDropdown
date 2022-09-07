﻿

using SimpleCrudDropdown.Contract;
using SimpleCrudDropdown.DAL;
using SimpleCrudDropdown.Models;

namespace SimpleCrudDropdown.Repositories
{
   public class CustomerRepository : Repository<Customer>, ICustomerRepository
   {
      public CustomerRepository(DataContext context) : base(context)
      {
      }

      public async Task<Customer> GetCustomerByID(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(c => c.OID == key && c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<Customer> GetCustomerByName(string customerName)
      {
         try
         {
            return await FirstOrDefaultAsync(c => c.Name == customerName && c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<Customer>> GetCustomers()
      {
         try
         {
            return await QueryAsync(c => c.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}
