using MvmSysApp.Data;
using MvmSysApp.Models;

namespace MvmSysApp;

// partial: esta classe se junta ao código gerado do XAML (VeiculosPrincipal.xaml).
// Herda ContentPage -> é uma tela do app.
public partial class VeiculosPrincipal : ContentPage
{
    public VeiculosPrincipal()
    {
        InitializeComponent(); // carrega o XAML e cria os controles (sbPesquisar, cvVeiculos...)
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
        // ?. e ??: se o texto for null, usa string vazia (evita NullReferenceException)
        string termo = sbPesquisar.Text?.Trim() ?? string.Empty;

        // LINQ encadeado:
        // Where   -> filtra (Contains ignora maiúsculas/minúsculas com OrdinalIgnoreCase)
        // OrderBy -> ordena por Placa
        // ToList  -> executa a consulta e gera uma List<Veiculo> para o ItemsSource
        bool somenteLicenciados = chkSomenteLicenciados.IsChecked;

        cvVeiculos.ItemsSource = Repositorios.Veiculos.Listar()
            .Where(x => x.Placa.Contains(termo, StringComparison.OrdinalIgnoreCase) || x.Modelo.Contains(termo, StringComparison.OrdinalIgnoreCase))
            .Where(x => !somenteLicenciados || x.Licenciado) // filtro do CheckBox
            .OrderBy(x => x.Placa)
            .ToList();
    }

    // Evento do SearchBar: a cada tecla digitada, recarrega a lista com o novo filtro
    private void sbPesquisarOnTextChanged(object? sender, TextChangedEventArgs e)
    {
        CarregarLista();
    }

    // Evento do CheckBox: recarrega a lista com/sem o filtro de licenciados
    private void chkSomenteLicenciadosOnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        CarregarLista();
    }

    // CREATE: abre a tela de inserção
    private async void btnInserirOnClick(object? sender, EventArgs e)
    {
        // PushAsync empilha a nova página por cima da atual (com animação)
        await Navigation.PushAsync(new VeiculosInserir());
    }

    // UPDATE: abre a tela de alteração enviando o item da linha clicada
    private async void btnAlterarOnClick(object? sender, EventArgs e)
    {
        // Pattern matching: verifica se sender é Button E se seu BindingContext é um Veiculo.
        // O BindingContext do botão é o item da lista (definido pelo DataTemplate).
        // Se não for, sai sem fazer nada (proteção contra clique inesperado).
        if (sender is not Button { BindingContext: Veiculo veiculo })
            return;

        // Passa o veiculo clicado para a tela de alteração preencher o formulário
        await Navigation.PushAsync(new VeiculosAlterar(veiculo));
    }

    // DELETE: pede confirmação e remove
    private async void btnExcluirOnClick(object? sender, EventArgs e)
    {
        if (sender is not Button { BindingContext: Veiculo veiculo })
            return;

        // DisplayAlertAsync retorna true (Confirmar) ou false (Voltar)
        bool confirmou = await DisplayAlertAsync(
            "Excluir",
            $"Confirma a exclusão do veículo {veiculo.Placa}?",
            "Confirmar",
            "Voltar");

        if (!confirmou)
            return;

        Repositorios.Veiculos.Excluir(veiculo.Id);
        CarregarLista(); // recarrega para o item sumir da tela
    }
}
