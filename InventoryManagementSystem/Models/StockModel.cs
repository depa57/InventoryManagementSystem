using System.ComponentModel;

namespace InventoryManagementSystem.Models
{
    public class StockModel
    {
        public int StockId { get; set; }
        [DisplayName("Supplier")]
       
        public int SupplierId{ get; set; }
        [DisplayName("Product")]
        public int ProductId { get; set; }
       
        public int Count{ get; set; }
       
        public decimal TotalCost { get; set; }
        public DateTime Date { get; set; }
    }
}
