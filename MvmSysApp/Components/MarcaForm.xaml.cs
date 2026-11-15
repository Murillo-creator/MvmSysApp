using MvmSysApp.Models;

namespace MvmSysApp.Components;

public partial class MarcaForm : ContentView
{
    private static readonly List<string> Paises = new()
    {
        "Alemanha", "Brasil", "Coreia do Sul", "Estados Unidos", "França",
        "Itália", "Japão", "Reino Unido", "Suécia", "Outro"
    };

    public MarcaForm()
    {
        InitializeComponent();
        pkPais.ItemsSource = Paises;
    }

    /// <summary>Joga os dados da entidade nos campos (usado na tela Alterar).</summary>
    public void Preencher(Marca marca)
    {
        entNome.Text = marca.Nome;
        pkPais.SelectedItem = Paises.Contains(marca.Pais) ? marca.Pais : null;
        swAtiva.IsToggled = marca.Ativa;
    }

    /// <summary>Valida os campos. Devolve a mensagem de erro quando inválido.</summary>
    public bool Validar(out string mensagem)
    {
        if (string.IsNullOrWhiteSpace(entNome.Text))
        {
            mensagem = "Informe o nome da marca.";
            return false;
        }

        // SelectedIndex = -1 significa que nada foi escolhido no Picker
        if (pkPais.SelectedIndex < 0)
        {
            mensagem = "Selecione o país de origem.";
            return false;
        }

        mensagem = string.Empty;
        return true;
    }

    /// <summary>Copia o que foi digitado/escolhido para a entidade.</summary>
    public void CopiarPara(Marca marca)
    {
        marca.Nome = (entNome.Text ?? string.Empty).Trim();
        marca.Pais = pkPais.SelectedItem as string ?? string.Empty;
        marca.Ativa = swAtiva.IsToggled;
    }
}
