using SQLite;
using QuickOrder.Models;
using System.Diagnostics;

namespace QuickOrder.Services
{
    public class DatabaseService
    {
        private const string DatabaseFilename = "QuickOrderData.db";
        private static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            _database = new SQLiteAsyncConnection(DatabasePath);
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            try
            {
                if (!File.Exists(DatabasePath))
                {
                    using var stream = await FileSystem.OpenAppPackageFileAsync(DatabaseFilename);
                    using var newStream = File.Create(DatabasePath);
                    await stream.CopyToAsync(newStream);
                    Debug.WriteLine("Banco de dados copiado para: " + DatabasePath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao inicializar banco: " + ex.Message);
            }
        }

        public async Task InsertClient(Client client)
        {
            try
            {
                await _database.InsertAsync(client);
                Debug.WriteLine("Cliente inserido com sucesso.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao inserir cliente: " + ex.Message);
            }
        }

        public async Task InsertProduct(Products product)
        {
            try
            {
                await _database.InsertAsync(product);
                Debug.WriteLine("Produto inserido com sucesso.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao inserir produto: " + ex.Message);
            }
        }

        public async Task InsertOrder(Order order)
        {
            try
            {
                await _database.RunInTransactionAsync(async tx =>
                {
                   
                    await _database.RunInTransactionAsync(tx =>
                    {
                        
                        tx.Insert(order);

                      
                        foreach (var item in order.Items)
                        {
                            item.IdOrder = order.IdOrder;
                            tx.Insert(item);
                        }
                    });
                    Debug.WriteLine("Pedido e itens inseridos com sucesso!");

                });
                Debug.WriteLine("Pedido e itens inseridos com sucesso!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao inserir pedido: " + ex.Message);
            }
        }
        public async Task<List<Client>> GetAllClients()
        {
            return await _database.Table<Client>().ToListAsync();
        }

        public async Task<Products> GetProductById(int id)
        {
            return await _database.Table<Products>().Where(p => p.IdProducts == id).FirstOrDefaultAsync();
        }
    }
}