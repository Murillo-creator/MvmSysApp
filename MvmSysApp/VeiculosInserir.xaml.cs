using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class VeiculosInserir : ContentPage
{
    public VeiculosInserir()
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
        // Validar retorna bool e preenche 'mensagem' via parâmetro out.
        // Se inválido, mostra o alerta e interrompe com return.
        if (!formVeiculo.Validar(out string mensagem))
        {
            await DisplayAlertAsync("Atenção", mensagem, "OK");
            return;
        }

        // Cria o objeto vazio e deixa o formulário copiar os campos digitados
        var veiculo = new Veiculo();
        formVeiculo.CopiarPara(veiculo);

        // O Id é gerado automaticamente dentro do repositório
        Repositorios.Veiculos.Inserir(veiculo);

        // PopAsync volta para a Principal, que recarrega a lista no OnAppearing
        await Navigation.PopAsync();
    }
}
