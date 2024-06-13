using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class ProductDetails
    {
       
        public int ProductId { get; set; }
        [StringLength(20, ErrorMessage = "The field must be a string with a maximum length of 20.")]
        [Required(ErrorMessage = "Prod_Name field is required.")]
       
        public string Prod_Name { get; set; }
        [Required(ErrorMessage = "Prod_Quantity field is required.")]
        public int Prod_Quantity { get; set; }
        [Required(ErrorMessage = "Prod_Price field is required.")]
        public decimal Prod_Price { get; set; }
    }
}
