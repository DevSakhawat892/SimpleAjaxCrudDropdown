using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCrudDropdown.Models
{
   public class ModelManager
   {
   }

   public class BaseModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int OID { get; set; }
      public bool IsDeleted { get; set; }
   }

   public class Customer : BaseModel
   {
      [Required(ErrorMessage = "Required")]
      [Display(Name = "Customer Name")]
      [StringLength(100)]
      public string Name { get; set; }

      [Required(ErrorMessage = "Required")]
      [StringLength(200)]
      public string Address { get; set; }

      [Required(ErrorMessage ="Required")]
      [Display(Name ="Cell Phone")]
      [StringLength(15)]
      public string CellPhone { get; set; }

      public virtual IEnumerable<Product> Products { get; set; }
   }

   public class Product : BaseModel
   {
      [Required(ErrorMessage = "Required")]
      [Display(Name = "Product Name")]
      [StringLength(100)]
      public string Name { get; set; }

      [Required(ErrorMessage = "Required")]
      [Display(Name = "Product Group")]
      [StringLength(200)]
      public string Group { get; set; }

      [Required(ErrorMessage = "Required")]
      [Display(Name = "Product Type")]
      public string Type { get; set; }

      [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Purchase Date")]
      public DateTime PurchaseDate { get; set; }

      public string? PhotoPath { get; set; }

      /// <summary>
      /// Foreignkey. Primary key of the table Customer.
      /// </summary>
      public int CustomerID { get; set; }

      [NotMapped]
      public IFormFile Photo { get; set; }

      /// <summary>
      /// Navigation property
      /// </summary>
      [ForeignKey("CustomerID")]
      public virtual Customer Customer { get; set; }
   }
}
