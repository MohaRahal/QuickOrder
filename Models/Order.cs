using SQLite;
using System;
using System.Collections.Generic;

namespace QuickOrder.Models
{
    [Table("ordersquick")]
    public class Order
    {
        [PrimaryKey, AutoIncrement]
        [Column("IdOrder")]
        public int IdOrder { get; set; }

        [Column("CnpjClient")]
        public string CnpjClient { get; set; }

        [Column("NameClient")]
        public string NameClient { get; set; }

        [Column("OrderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column("TotalOrder")]
        public decimal TotalOrder { get; set; }

        [Ignore]
        public List<OrderItem> Items { get; set; } = new();

        public Order() { }

        public Order(string cnpjClient, string nameClient, decimal totalOrder)
        {
            CnpjClient = cnpjClient;
            NameClient = nameClient;
            TotalOrder = totalOrder;
        }

        public Order(int idOrder, string cnpjClient, string nameClient, DateTime orderDate, decimal totalOrder)
            : this(cnpjClient, nameClient, totalOrder)
        {
            IdOrder = idOrder;
            OrderDate = orderDate;
        }
    }
}
