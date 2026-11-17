using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class MarcasInserir : ContentPage
{
    private readonly MarcaRepository _marcas;

    public MarcasInserir(MarcaRepository marcas)
    {
        InitializeComponent();
        _marcas = marcas;
    }

    // VOLTAR: desempilha a página atual e retorna para a Principal
    private async void btVoltarOnClick(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // SALVAR (Create): valida, grava no repositório e volta
    private async void btSalvarOnClick(object? sender, EventArgs e)
    {
        if (!formMarca.Validar(out string mensagem))
        {
            await DisplayAlertAsync("Atenção", mensagem, "OK");
            return;
        }

        var marca = new Marca();
        formMarca.CopiarPara(marca);

        await _marcas.SalvarAsync(marca);

        await Navigation.PopAsync();
    }
}
