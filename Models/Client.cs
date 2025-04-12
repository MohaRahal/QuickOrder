using SQLite;

namespace QuickOrder.Models
{
    [Table("clientquick")] 
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        [Column("idClient")] 
        public int IdClients { get; set; }

        [Column("CnpjClient")] 
        public string CNPJ { get; set; }

        [Column("NameClient")] 
        public string Name { get; set; }

  
        public Client() { }

       
        public Client(string cnpj, string name)
        {
            CNPJ = cnpj;
            Name = name;
        }
    }
}