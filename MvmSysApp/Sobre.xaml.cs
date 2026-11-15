using System.Linq.Expressions;

namespace MvmSysApp;

public partial class Sobre : ContentPage
{
    public Sobre()
    {
        InitializeComponent();
        lblVersao.Text = $"Versão {AppInfo.Current.VersionString}";
    }

    private async void BtnVoltarHomeOnClick(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
