using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class SupplierDetails
    {
        public int SupplierID { get; set; }

        [StringLength(20, ErrorMessage = "The field must be a string with a maximum length of 20.")]
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
    }
}
