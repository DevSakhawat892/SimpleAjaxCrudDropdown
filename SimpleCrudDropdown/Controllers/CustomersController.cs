using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SimpleCrudDropdown.Constants;
using SimpleCrudDropdown.Contracts;
using SimpleCrudDropdown.DAL;
using SimpleCrudDropdown.Models;

namespace SimpleCrudDropdown.Controllers
{
   public class CustomersController : Controller
   {
      private readonly IUnitOfWork context;
      private DataContext _context;

      public CustomersController(IUnitOfWork context, DataContext _context)
      {
         this.context = context;
         this._context = _context;
      }

      [HttpGet]
      public IActionResult CreateCustomer()
      {
         var model = new Customer();
         return View(model);
      }

      // POST: Customers/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> CreateCustomer(Customer customer)
      {
         try
         {
            if (await IsCustomerExists(customer) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.CustomerRepository.Add(customer);
            context.SaveChanges();

            return RedirectToAction("ReadCustomers");
            //return View(customer);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      // GET: Customers
      [HttpGet]
      public IActionResult GetCustomers()
      {
         try
         {
            var customer = _context.Customers.ToList();
            //var customerList = JsonConvert.SerializeObject(customer);
            return Json(new { data = customer});
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }
      // GET: Customers
      [HttpGet]
      public async Task<IActionResult> ReadCustomers()
      {
         try
         {
            var customer = await context.CustomerRepository.GetCustomers();
            return View(customer);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpGet]
      public async Task<IActionResult> ReadCustomerByKey(int key)
      {
         if (key <= 0)
            return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

         var customer = await context.CustomerRepository.GetCustomerByID(key);

         if (customer == null)
            return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

         return View(customer);
      }

      [HttpGet]
      public async Task<IActionResult> UpdateCustomer(int key)
      {
         if(key <= 0)
            return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

         var product = await context.CustomerRepository.GetCustomerByID(key);

         if (product == null)
            return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

         return View(product);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> UpdateCustomer(Customer customer)
      {
         try
         {
            if (await IsCustomerExists(customer) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

            context.CustomerRepository.Update(customer);
            context.SaveChanges();

            return RedirectToAction("ReadCustomers");
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Soft delte
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      [HttpGet]
      public async Task<IActionResult> SoftDeleteCustomer(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var customerInDb = await context.CustomerRepository.GetCustomerByID(key);

            if (customerInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            customerInDb.IsDeleted = true;

            context.CustomerRepository.Update(customerInDb);
            context.SaveChanges();

            return RedirectToAction("ReadCustomers");
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }


      [HttpGet]
      public async Task<IActionResult> Delete(int id)
      {
         if (id <= 0)
            return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

         var customerInDb = await context.CustomerRepository.GetCustomerByID(id);

         if (customerInDb == null)
            return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

         return View(customerInDb);
      }

      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {

         var customerInDb = await context.CustomerRepository.GetCustomerByID(id);

         if (customerInDb == null)
            return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

         if (customerInDb != null)
            context.CustomerRepository.Delete(customerInDb);
         context.SaveChanges();

         return RedirectToAction(nameof(ReadCustomers));
      }

      private async Task<bool> IsCustomerExists(Customer customer)
      {
         try
         {
            var customerInDb = await context.CustomerRepository.GetCustomerByName(customer.Name);

            if (customerInDb != null)
               if (customerInDb.OID != customer.OID)
                  return false;

            return false;
         }
         catch (Exception)
         {
            throw;
         }
      }

   }
}
