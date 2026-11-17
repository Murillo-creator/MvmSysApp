using Microsoft.Extensions.DependencyInjection;

namespace MvmSysApp;

public partial class AppShell : Shell
{
    public AppShell(
        IServiceProvider services)
    {
        InitializeComponent();

        foreach (var flyoutItem in Items.OfType<FlyoutItem>())
        {
            foreach (var section in flyoutItem.Items)
            {
                foreach (var content in section.Items)
                {
                    switch (content.Route)
                    {
                        case "MarcasPrincipal":
                            content.Content =
                                services.GetRequiredService<MarcasPrincipal>();
                            break;

                        case "ModelosPrincipal":
                            content.Content =
                                services.GetRequiredService<ModelosPrincipal>();
                            break;

                        case "VeiculosPrincipal":
                            content.Content =
                                services.GetRequiredService<VeiculosPrincipal>();
                            break;
                    }
                }
            }
        }
    }
}