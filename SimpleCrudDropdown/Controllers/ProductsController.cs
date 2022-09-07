using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleCrudDropdown.DAL;
using SimpleCrudDropdown.Models;

namespace SimpleCrudDropdown.Controllers
{
   public class ProductsController : Controller
   {
      private readonly DataContext _context;
      public ProductsController(DataContext context)
      {
         _context = context;
      }

      // GET: Products

      public IActionResult Index()
      {
         return View();
      }
      public async Task<IActionResult> GetProducts()
      {
         //var dataContext = _context.Products.Include(p => p.Customer);
         //return View(await dataContext.ToListAsync());

         var allData = await _context.Products.ToListAsync();
         return Json(new { data = allData });
      }

      public IActionResult GetByID(int id)
      {
         var product = _context.Products.Find(id);
         return Json(new { data = product });
      }

      public JsonResult Update(Product product, IFormFile Photo)
      {
         if (Photo != null)
         {
            string fPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");
            if (!Directory.Exists(fPath))
            {
               Directory.CreateDirectory(fPath);
            }

            string fExt = Path.GetExtension(Photo.FileName).ToLower();
            if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
            {
               string fNameWithoutSpace = product.Name.Replace(" ", "_");
               string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
               "") + fExt;
               string fileToSave = Path.Combine(fPath, fName);
               using (var fileStream = new FileStream(fileToSave, FileMode.Create))
               {
                  Photo.CopyTo(fileStream);
                  product.PhotoPath = "~/Images/" + fName;
               }
            }
         }
         _context.Products.Update(product);
         _context.SaveChanges();
         return Json(new { data = product });
      }
      public JsonResult Save(Product product)
      {

         if (product.Photo != null)
         {
            string fPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");
            if (!Directory.Exists(fPath))
            {
               Directory.CreateDirectory(fPath);
            }

            string fExt = Path.GetExtension(product.Photo.FileName).ToLower();
            if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
            {
               string fNameWithoutSpace = product.Name.Replace(" ", "_");
               string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
               "") + fExt;
               string fileToSave = Path.Combine(fPath, fName);
               using (var fileStream = new FileStream(fileToSave, FileMode.Create))
               {
                  product.Photo.CopyTo(fileStream);
                  product.PhotoPath = "~/Images/" + fName;
               }
            }
         }

         var productEntity = _context.Products.Add(product);
         _context.SaveChanges();
         return Json(new { data = productEntity });
         //return Json(product);
      }

      [HttpPost]
      public IActionResult GetByIDDelete(int id)
      {
         var product = _context.Products.Find(id);
         _context.Products.Remove(product);
         _context.SaveChanges();
         return Json(new { data = product });
      }

      // GET: Products/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || _context.Products == null)
         {
            return NotFound();
         }

         var product = await _context.Products
             .Include(p => p.Customer)
             .FirstOrDefaultAsync(m => m.OID == id);
         if (product == null)
         {
            return NotFound();
         }

         return View(product);
      }

      // GET: Products/Create
      public IActionResult Create()
      {
         ViewData["CustomerID"] = new SelectList(_context.Customers, "OID", "Name");
         return View();
      }

      // POST: Products/Create        
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(Product product)
      {
         //if (product.Photo != null)
         if (product.Photo != null)
         {
            string fPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");
            if (!Directory.Exists(fPath))
            {
               Directory.CreateDirectory(fPath);
            }

            string fExt = Path.GetExtension(product.Photo.FileName).ToLower();
            if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
            {
               string fNameWithoutSpace = product.Name.Replace(" ", "_");
               string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
               "") + fExt;
               string fileToSave = Path.Combine(fPath, fName);
               using (var fileStream = new FileStream(fileToSave, FileMode.Create))
               {
                  product.Photo.CopyTo(fileStream);
                  product.PhotoPath = "~/Images/" + fName;
               }
            }
         }

         //if (product.Photo != null)
         //{
         //   string fPath = Path.Combine(hosting.WebRootPath, "Images\\Products");
         //   if (!Directory.Exists(fPath))
         //   {
         //      Directory.CreateDirectory(fPath);
         //   }
         //   string fExt = Path.GetExtension(product.Photo.FileName).ToLower();
         //   if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
         //   {
         //      string fNameWithoutSpace = product.Name.Replace(" ", "_");
         //      string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
         //      "") + fExt;
         //      string fileToSave = Path.Combine(fPath, fName);
         //      using (var fileStream = new FileStream(fileToSave, FileMode.Create))
         //      {
         //         product.Photo.CopyTo(fileStream);
         //         product.PhotoPath = "~/Images/Products/" + fName;
         //      }
         //   }
         //}
         _context.Add(product);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
         ViewData["CustomerID"] = new SelectList(_context.Customers, "OID", "Name", product.CustomerID);
         return View(product);
      }

      // GET: Products/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || _context.Products == null)
         {
            return NotFound();
         }

         var product = await _context.Products.FindAsync(id);
         if (product == null)
         {
            return NotFound();
         }
         ViewData["CustomerID"] = new SelectList(_context.Customers, "OID", "Address", product.CustomerID);
         return View(product);
      }

      // POST: Products/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Name,Group,Type,PurchaseDate,PhotoPath,CustomerID,OID,IsDeleted")] Product product)
      {
         if (id != product.OID)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(product);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ProductExists(product.OID))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         ViewData["CustomerID"] = new SelectList(_context.Customers, "OID", "Address", product.CustomerID);
         return View(product);
      }

      // GET: Products/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || _context.Products == null)
         {
            return NotFound();
         }

         var product = await _context.Products
             .Include(p => p.Customer)
             .FirstOrDefaultAsync(m => m.OID == id);
         if (product == null)
         {
            return NotFound();
         }

         return View(product);
      }

      // POST: Products/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         if (_context.Products == null)
         {
            return Problem("Entity set 'DataContext.Products'  is null.");
         }
         var product = await _context.Products.FindAsync(id);
         if (product != null)
         {
            _context.Products.Remove(product);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ProductExists(int id)
      {
         return (_context.Products?.Any(e => e.OID == id)).GetValueOrDefault();
      }
   }
}
