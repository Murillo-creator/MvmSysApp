using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

public partial class ModelosPrincipal : ContentPage
{
    public ModelosPrincipal()
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

        cvModelos.ItemsSource = Repositorios.Modelos.Listar()
            .Where(x => x.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase) || x.Categoria.Contains(termo, StringComparison.OrdinalIgnoreCase))
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
        await Navigation.PushAsync(new ModelosInserir());
    }

    // UPDATE: abre a tela de alteração enviando o item da linha clicada
    private async void btnAlterarOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Modelo modelo })
            return;

        await Navigation.PushAsync(new ModelosAlterar(modelo));
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

        Repositorios.Modelos.Excluir(modelo.Id);
        CarregarLista();
    }
}
