using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class MarcasPrincipal : ContentPage
{
    private readonly MarcaRepository _marcas;

    public MarcasPrincipal(MarcaRepository marcas)
    {
        InitializeComponent();
        _marcas = marcas;
    }
    // Roda toda vez que a tela aparece, inclusive ao voltar de Inserir/Alterar.
    // É isso que faz a lista atualizar sozinha depois de salvar.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarListaAsync();
    }

// READ: busca no repositório, aplica o filtro da pesquisa e mostra na lista
    private async Task CarregarListaAsync()
    {
        string termo = sbPesquisar.Text?.Trim() ?? string.Empty;
        var marcas = await _marcas.ListarAsync();

        cvMarcas.ItemsSource = marcas
            .Where(m =>
                m.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
                m.Pais.Contains(termo, StringComparison.OrdinalIgnoreCase))
            .OrderBy(m => m.Nome)
            .ToList();
    }

    private async void sbPesquisarOnTextChanged(object? sender, TextChangedEventArgs e)
    {
        await CarregarListaAsync();
    }

    // CREATE: abre a tela de inserção
    private async void btnInserirOnClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MarcasInserir(_marcas));
    }

    // UPDATE: abre a tela de alteração enviando o item da linha clicada
    private async void btnAlterarOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Marca marca })
            return;

        await Navigation.PushAsync(new MarcasAlterar(_marcas, marca));
    }

    // DELETE: pede confirmação e remove
    private async void btnExcluirOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Marca marca })
            return;

        bool confirmou = await DisplayAlertAsync(
            "Excluir",
            $"Confirma a exclusão da marca {marca.Nome}?",
            "Confirmar",
            "Voltar");

        if (!confirmou)
            return;

        await _marcas.ExcluirAsync(marca.Id);
        await CarregarListaAsync();
    }
}
