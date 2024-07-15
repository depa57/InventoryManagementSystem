using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class StockModel
    {
        public int PurchaseId { get; set; }
        public string SupplierName { get; set; }
        [DisplayName("Supplier")]
        public int SupplierId{ get; set; }
        public string ProductName { get; set; }
        [DisplayName("Product")]

        public int ProductId { get; set; }
        public int Count{ get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
