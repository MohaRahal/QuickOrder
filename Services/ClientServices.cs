using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using QuickOrder.Models;
using SQLite;

namespace QuickOrder.Services
{
    public class ClientServices
    {
        private readonly SQLiteAsyncConnection _db;

        public ClientServices()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "QuickOrderData.db");
            CopyDatabaseIfNotExists("QuickOrderData.db", dbPath).Wait();
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Client>().Wait();
        }

        private async Task CopyDatabaseIfNotExists(string fileName, string destinationPath)
        {
            if (!File.Exists(destinationPath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var dest = File.Create(destinationPath);
                await stream.CopyToAsync(dest);
            }
        }

        public async Task SalvarClientAsync(Client client)
        {
            var existing = await _db.Table<Client>()
                .Where(c => c.CNPJ == client.CNPJ)
                .FirstOrDefaultAsync();

            if (existing != null)
                return; 

            await _db.InsertAsync(client);
        }

        public Task<List<Client>> BuscarTodosClientesAsync()
        {
            return _db.Table<Client>().ToListAsync();
        }

        public Task RemoverClientAsync(int id)
        {
            return _db.DeleteAsync<Client>(id);
        }

        public Task UpdateClientAsync(Client client)
        {
            return _db.UpdateAsync(client);
        }
    }
}
