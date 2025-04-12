using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuickOrder.Models;
using SQLite;

namespace QuickOrder.Services
{
    public class ProdutosServices
    {
        private readonly SQLiteAsyncConnection _database;

        public ProdutosServices(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Products>().Wait();
        }

        public async Task<List<Products>> GetProductsAsync()
        {
            try
            {
                return await _database.Table<Products>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produtos: " + ex.Message);
            }
        }
            
        public async Task AddProductAsync(Products product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Nome do produto é obrigatório.");

            try
            {
                await _database.InsertAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar produto: " + ex.Message);
            }
        }

        public async Task RemoveProductAsync(int id)
        {
            try
            {
                await _database.DeleteAsync<Products>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover produto: " + ex.Message);
            }
        }

        public async Task UpdateProductAsync(Products product)
        {
            try
            {
                await _database.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar produto: " + ex.Message);
            }
        }
    }
}
