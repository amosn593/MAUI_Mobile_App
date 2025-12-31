using CommunityToolkit.Maui;
using DemoApp.Pages;
using DemoApp.Services;
using DemoApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace DemoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri("https://api.yoursite.com/api/")
                };
            });

            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();

            builder.Services.AddTransient<CreateOrderPage>();
            builder.Services.AddTransient<CreateOrderViewModel>();
            builder.Services.AddTransient<SelectCustomerPage>();
            builder.Services.AddTransient<SelectCustomerViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
