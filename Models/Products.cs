using SQLite;

namespace QuickOrder.Models
{
    [Table("productsquick")]
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        [Column("IdProduct")] 
        public int IdProducts { get; set; }

        [Column("NameProduct")]
        public string Name { get; set; } = string.Empty;

        [Column("DescriptionProduct")]
        public string Description { get; set; } = string.Empty;

        [Column("PriceProduct")]
        public decimal Price { get; set; }

        [Column("StockProduct")]
        public int Stock { get; set; }

        public Products() { }

   
        public Products(string name, string description, decimal price, int stock = 0)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}