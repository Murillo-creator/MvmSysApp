using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class MarcasPrincipal : ContentPage
{
    public MarcasPrincipal()
    {
        InitializeComponent();
    }

    // Roda toda vez que a tela aparece, inclusive ao voltar de Inserir/Alterar.
    // É isso que faz a lista atualizar sozinha depois de salvar.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarLista();
    }

    // READ: busca no repositório, aplica o filtro da pesquisa e mostra na lista
    private void CarregarLista()
    {
        string termo = sbPesquisar.Text?.Trim() ?? string.Empty;

        cvMarcas.ItemsSource = Repositorios.Marcas.Listar()
            .Where(x => x.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) || x.Pais.Contains(termo, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Nome)
            .ToList();
    }

    private void sbPesquisarOnTextChanged(object? sender, TextChangedEventArgs e)
    {
        CarregarLista();
    }

    // CREATE: abre a tela de inserção
    private async void btnInserirOnClick(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MarcasInserir());
    }

    // UPDATE: abre a tela de alteração enviando o item da linha clicada
    private async void btnAlterarOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Marca marca })
            return;

        await Navigation.PushAsync(new MarcasAlterar(marca));
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

        Repositorios.Marcas.Excluir(marca.Id);
        CarregarLista();
    }
}
