namespace InventoryManagementSystem.Models
{
    public class StockModel
    {
        public int StockId { get; set; }
        public int SupplierId{ get; set; }
        public int ProductId { get; set; }
        public int Count{ get; set; }
        public decimal Cost { get; set; }
    }
}
