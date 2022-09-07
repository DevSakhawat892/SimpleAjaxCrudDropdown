using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleCrudDropdown.Domain.Models
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
   }

   public class Product : BaseModel
   {
      [Required(ErrorMessage = "Required")]
      [Display(Name = "Product Name")]
      [StringLength(100)]
      public string Name { get; set; }

      [Required(ErrorMessage = "Required")]
      [Display(Name ="Product Group")]
      [StringLength(200)]
      public string Group { get; set; }

      [Required(ErrorMessage ="Required")]
      [Display(Name ="Product Type")]
      public string Type { get; set; }
   }
}
