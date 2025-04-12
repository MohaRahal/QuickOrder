using Microsoft.Extensions.Logging;
using QuestPDF.Infrastructure;
using QuickOrder.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.Maui.LifecycleEvents;

namespace QuickOrder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            QuestPDF.Settings.License = LicenseType.Community;

            builder.Services.AddMauiBlazorWebView();

            //SERVICES
            builder.Services.AddSingleton<PedidoPdfServices>();
            builder.Services.AddSingleton<CnpjApiServices>();
            builder.Services.AddSingleton<ClientServices>();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "QuickOrderData.db");
            builder.Services.AddSingleton(new OrderService(dbPath));
            builder.Services.AddSingleton(new ProdutosServices(dbPath));

//PARA DEIXAR A TELA FULLSCREEN
#if WINDOWS

            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(windowsLifecycleBuilder =>
                {
                    windowsLifecycleBuilder.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false;

                        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);

                        switch (appWindow.Presenter)
                        {
                            case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                                overlappedPresenter.SetBorderAndTitleBar(true, true); // deixa com borda e barra
                                overlappedPresenter.Maximize(); // inicia maximizado
                                break;
                        }
                    });
                });
            });
#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
