using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class VeiculosAlterar : ContentPage
{
    // readonly: o campo só pode ser atribuído no construtor.
    // Guarda a referência do veículo que veio da tela Principal.
    private readonly VeiculoRepository _veiculos;
    private readonly Veiculo _veiculo;

    // Recebe o item escolhido na tela Principal (injeção via construtor)
    public VeiculosAlterar(VeiculoRepository veiculos, Veiculo veiculo)
    {
        InitializeComponent();

        _veiculos = veiculos;
        _veiculo = veiculo;

        formVeiculo.Preencher(_veiculo); // mostra os dados atuais nos campos
    }

    // VOLTAR: descarta o que foi digitado (o objeto só é alterado no Salvar)
    private async void btVoltarOnClick(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    // SALVAR (Update): valida, atualiza no repositório e volta
    private async void btSalvarOnClick(object? sender, EventArgs e)
    {
        if (!formVeiculo.Validar(out string mensagem))
        {
            await DisplayAlertAsync("Atenção", mensagem, "OK");
            return;
        }

        // Copia os campos editados para o MESMO objeto recebido no construtor
        formVeiculo.CopiarPara(_veiculo);

        // O repositório localiza pelo Id e substitui o registro
        await _veiculos.SalvarAsync(_veiculo);

        await Navigation.PopAsync();
    }
}
