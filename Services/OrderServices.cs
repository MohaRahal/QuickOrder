using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using QuickOrder.Models;
using SQLite;

public class OrderService
{
    private readonly SQLiteAsyncConnection _connection;

    public OrderService(string dbPath)
    {
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<Order>().Wait();
        _connection.CreateTableAsync<OrderItem>().Wait();
    }

    public async Task<List<Order>> ObterTodosPedidosAsync()
    {
        var pedidos = await _connection.Table<Order>().OrderByDescending(o => o.OrderDate).ToListAsync();

        foreach (var pedido in pedidos)
        {
            var itens = await _connection.Table<OrderItem>()
                .Where(i => i.IdOrder == pedido.IdOrder)
                .ToListAsync();

            pedido.Items = itens;
        }

        return pedidos;
    }

    public async Task<(bool Success, string Message)> SalvarPedidoAsync(Order order)
    {
        try
        {
            await _connection.RunInTransactionAsync(tran =>
            {
                var problemas = VerificarEstoque(tran, order.Items);
                if (problemas.Count > 0)
                    throw new EstoqueInsuficienteException(FormatarMensagemEstoqueInsuficiente(problemas));

                tran.Insert(order);
                long novoPedidoId = tran.ExecuteScalar<long>("SELECT last_insert_rowid()");

                foreach (var item in order.Items)
                {
                    item.IdOrder = (int)novoPedidoId;
                    tran.Insert(item);
                    AtualizarEstoque(tran, item);
                }
            });

            return (true, "Pedido salvo com sucesso!");
        }
        catch (EstoqueInsuficienteException ex)
        {
            return (false, ex.Message);
        }
        catch (Exception ex)
        {
            return (false, $"Erro ao salvar pedido: {ex.Message}");
        }
    }

    private List<ProblemaEstoque> VerificarEstoque(SQLiteConnection conn, List<OrderItem> itens)
    {
        var problemas = new List<ProblemaEstoque>();

        foreach (var item in itens)
        {
            var result = conn.Query<ProdutoEstoque>("SELECT IdProduct, NameProduct, StockProduct FROM productsquick WHERE IdProduct = ?", item.IdProduct);

            if (result.Count > 0)
            {
                var prod = result[0];
                if (prod.StockProduct < item.Quantity)
                {
                    problemas.Add(new ProblemaEstoque(
                        NomeProduto: prod.NameProduct,
                        QuantidadePedido: item.Quantity,
                        EstoqueDisponivel: prod.StockProduct
                    ));
                }
            }
        }

        return problemas;
    }


    private void AtualizarEstoque(SQLiteConnection conn, OrderItem item)
    {
        conn.Execute(
            "UPDATE productsquick SET StockProduct = StockProduct - ? WHERE IdProduct = ?",
            item.Quantity, item.IdProduct);
    }
    private string FormatarMensagemEstoqueInsuficiente(List<ProblemaEstoque> problemas)
    {
        var mensagem = "Estoque insuficiente para os seguintes produtos:\n\n";
        foreach (var p in problemas)
        {
            mensagem += $"- {p.NomeProduto}: Pedido {p.QuantidadePedido}, Estoque {p.EstoqueDisponivel}\n";
        }
        return mensagem + "\nAjuste as quantidades e tente novamente.";
    }

    private record ProblemaEstoque(string NomeProduto, int QuantidadePedido, int EstoqueDisponivel);

    private class ProdutoEstoque
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public int StockProduct { get; set; }
    }

    private class EstoqueInsuficienteException : Exception
    {
        public EstoqueInsuficienteException(string message) : base(message) { }
    }
}
