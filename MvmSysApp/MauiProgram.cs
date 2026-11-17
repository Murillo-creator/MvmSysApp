using MvmSysApp.Data;
using MvmSysApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MvmSysApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<AppDatabase>();

            builder.Services.AddSingleton<MarcaRepository>();
            builder.Services.AddSingleton<ModeloRepository>();
            builder.Services.AddSingleton<VeiculoRepository>();

            builder.Services.AddTransient<MarcasPrincipal>();
            builder.Services.AddTransient<ModelosPrincipal>();
            builder.Services.AddTransient<VeiculosPrincipal>();

            builder.Services.AddSingleton<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
