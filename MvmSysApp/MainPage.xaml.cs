namespace MvmSysApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Toque num card da Home: navega para a rota enviada em CommandParameter.
    // "//" = rota absoluta (troca a aba/seção atual do Shell, sem empilhar).
    private async void OnOpcaoTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is string rota)
            await Shell.Current.GoToAsync($"//{rota}");
    }
}
