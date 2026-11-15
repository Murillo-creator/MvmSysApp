using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class ModelosInserir : ContentPage
{
    public ModelosInserir()
    {
        InitializeComponent();
    }

    // VOLTAR: desempilha a página atual e retorna para a Principal
    private async void btVoltarOnClick(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // SALVAR (Create): valida, grava no repositório e volta
    private async void btSalvarOnClick(object? sender, EventArgs e)
    {
        if (!formModelo.Validar(out string mensagem))
        {
            await DisplayAlertAsync("Atenção", mensagem, "OK");
            return;
        }

        var modelo = new Modelo();
        formModelo.CopiarPara(modelo);

        Repositorios.Modelos.Inserir(modelo);

        await Navigation.PopAsync();
    }
}
