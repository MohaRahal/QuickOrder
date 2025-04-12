using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuickOrder.Models;

namespace QuickOrder.Services
{
    public class PedidoPdfServices
    {
        public byte[] GerarPdf(List<Order> pedidos)
        {
            try {
                var pdf = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(30);
                        page.DefaultTextStyle(x => x.FontSize(12));

                        page.Header().Text("📦 Relatório de Pedidos").SemiBold().FontSize(18);

                        page.Content().Column(col =>
                        {
                            foreach (var pedido in pedidos)
                            {
                                col.Item().PaddingBottom(10).BorderBottom(1).Column(pedidoCol =>
                                {
                                    pedidoCol.Item().Text($"🧑 Cliente: {pedido.NameClient}");
                                    pedidoCol.Item().Text($"🏢 CNPJ: {pedido.CnpjClient}");
                                    pedidoCol.Item().Text("🛒 Produtos:");
                                    foreach (var item in pedido.Items)
                                    {
                                        pedidoCol.Item().Text($"  - {item.ProductName} - {item.Quantity} x {item.PriceUnit:C}");
                                    }
                                    pedidoCol.Item().Text($"💰 Total: {pedido.TotalOrder:C}").Bold();
                                    pedidoCol.Item().Text(" ");
                                });
                            }
                        });

                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.Span($"Gerado por QuickOrder - ");
                            x.CurrentPageNumber();
                            x.Span(" de ");
                            x.TotalPages();
                        });
                    });
                });

                return pdf.GeneratePdf();
            }catch(Exception ex)
            {
                Console.WriteLine($"Erro ao gerar PDF: {ex.Message}");
                throw ex;
            }
        }
    }
}


