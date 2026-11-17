using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class MarcasAlterar : ContentPage
{
    private readonly MarcaRepository _marcas;
    private readonly Marca _marca;

    // Recebe o item escolhido na tela Principal
    public MarcasAlterar(MarcaRepository marcas, Marca marca)
    {
        InitializeComponent();

        _marcas = marcas;
        _marca = marca;

        formMarca.Preencher(_marca);
    }

    // VOLTAR: descarta o que foi digitado (o objeto só é alterado no Salvar)
    private async void btVoltarOnClick(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // SALVAR (Update): valida, atualiza no repositório e volta
    private async void btSalvarOnClick(object? sender, EventArgs e)
    {
        if (!formMarca.Validar(out string mensagem))
        {
            await DisplayAlertAsync("Atenção", mensagem, "OK");
            return;
        }

        formMarca.CopiarPara(_marca);

        await _marcas.SalvarAsync(_marca);
        await Navigation.PopAsync();
    }
}
