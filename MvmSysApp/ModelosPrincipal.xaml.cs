using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class ModelosPrincipal : ContentPage
{
    private readonly ModeloRepository _modelos;

    public ModelosPrincipal(ModeloRepository modelos)
    {
        InitializeComponent();
        _modelos = modelos;
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
        var modelos = await _modelos.ListarAsync();

        cvModelos.ItemsSource = modelos
            .Where(m => m.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) || 
            m.Categoria.Contains(termo, StringComparison.OrdinalIgnoreCase))
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
        await Navigation.PushAsync(new ModelosInserir(_modelos));
    }

    // UPDATE: abre a tela de alteração enviando o item da linha clicada
    private async void btnAlterarOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Modelo modelo })
            return;

        await Navigation.PushAsync(new ModelosAlterar(_modelos, modelo));
    }

    // DELETE: pede confirmação e remove
    private async void btnExcluirOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Modelo modelo })
            return;

        bool confirmou = await DisplayAlertAsync(
            "Excluir",
            $"Confirma a exclusão do modelo {modelo.Nome}?",
            "Confirmar",
            "Voltar");

        if (!confirmou)
            return;

        await _modelos.ExcluirAsync(modelo.Id);
        await CarregarListaAsync();
    }
}
