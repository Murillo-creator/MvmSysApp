using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class ModelosAlterar : ContentPage
{
    private readonly Modelo _modelo;

    // Recebe o item escolhido na tela Principal
    public ModelosAlterar(Modelo modelo)
    {
        InitializeComponent();

        _modelo = modelo;
        formModelo.Preencher(_modelo);
    }

    // VOLTAR: descarta o que foi digitado (o objeto só é alterado no Salvar)
    private async void btVoltarOnClick(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // SALVAR (Update): valida, atualiza no repositório e volta
    private async void btSalvarOnClick(object? sender, EventArgs e)
    {
        if (!formModelo.Validar(out string mensagem))
        {
            await DisplayAlertAsync("Atenção", mensagem, "OK");
            return;
        }

        formModelo.CopiarPara(_modelo);
        Repositorios.Modelos.Alterar(_modelo);

        await Navigation.PopAsync();
    }
}
