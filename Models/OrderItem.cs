using SQLite;

namespace QuickOrder.Models
{
    [Table("orderitemsquick")]
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        [Column("IdItem")]
        public int IdItem { get; set; }

        [Column("IdOrder")]
        public int IdOrder { get; set; }

        [Column("IdProduct")]
        public int IdProduct { get; set; }

        [Column("ProductName")]
        public string ProductName { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("PriceUnit")]
        public decimal PriceUnit { get; set; }

        [Column("Subtotal")]
        public decimal Subtotal { get; set; }

        public OrderItem() { }

        public OrderItem(int idOrder, int idProduct, string productName, int quantity, decimal priceUnit)
        {
            IdOrder = idOrder;
            IdProduct = idProduct;
            ProductName = productName;
            Quantity = quantity;
            PriceUnit = priceUnit;
            Subtotal = quantity * priceUnit;
        }
    }
}
