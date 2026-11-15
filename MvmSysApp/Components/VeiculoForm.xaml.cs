using MvmSysApp.Models;

namespace MvmSysApp.Components;

// ContentView (não ContentPage): é um componente reutilizável,
// embutido dentro das páginas Inserir e Alterar -> evita código duplicado.
public partial class VeiculoForm : ContentView
{
    public VeiculoForm()
    {
        InitializeComponent();
        dpAquisicao.MaximumDate = DateTime.Today; // não permite data futura
    }

    // Lê a data do DatePicker. Escrito com "is DateTime" para funcionar
    // tanto se Date for DateTime quanto DateTime? (mudou em versões do MAUI).
    private DateTime DataEscolhida() => dpAquisicao.Date is DateTime d ? d : DateTime.Today;

    // Usado pela tela ALTERAR: joga os dados do objeto nos campos do formulário
    public void Preencher(Veiculo veiculo)
    {
        entPlaca.Text = veiculo.Placa;
        entModelo.Text = veiculo.Modelo;
        entCor.Text = veiculo.Cor;
        // Entry só aceita string -> converte o int; se for 0, deixa vazio
        entAno.Text = veiculo.Ano > 0 ? veiculo.Ano.ToString() : string.Empty;
        dpAquisicao.Date = veiculo.DataAquisicao < dpAquisicao.MinimumDate ? DateTime.Today : veiculo.DataAquisicao;
        chkLicenciado.IsChecked = veiculo.Licenciado;
        edtObservacoes.Text = veiculo.Observacoes;
    }

    // Valida os campos. Retorna true/false e entrega a mensagem de erro
    // pelo parâmetro 'out' (quem chama usa: Validar(out string mensagem))
    public bool Validar(out string mensagem)
    {
        // IsNullOrWhiteSpace: pega null, vazio e texto só com espaços
        if (string.IsNullOrWhiteSpace(entPlaca.Text))
        {
            mensagem = "Informe a placa.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(entModelo.Text))
        {
            mensagem = "Informe o modelo.";
            return false;
        }

        // TryParse: tenta converter texto em int sem lançar exceção.
        // Também rejeita ano menor que 1900 ou maior que o próximo ano.
        if (!int.TryParse(entAno.Text, out int ano) || ano < 1900 || ano > DateTime.Now.Year + 1)
        {
            mensagem = "Informe um ano válido.";
            return false;
        }

        mensagem = string.Empty;
        return true;
    }

    // Transfere o que foi digitado nos campos para as propriedades do objeto.
    // (entX.Text ?? string.Empty) protege contra null; Trim() tira espaços das pontas.
    public void CopiarPara(Veiculo veiculo)
    {
        veiculo.Placa = (entPlaca.Text ?? string.Empty).Trim().ToUpperInvariant(); // placa sempre MAIÚSCULA
        veiculo.Modelo = (entModelo.Text ?? string.Empty).Trim();
        veiculo.Cor = (entCor.Text ?? string.Empty).Trim();
        int.TryParse(entAno.Text, out int ano); // se falhar, ano fica 0
        veiculo.Ano = ano;
        veiculo.DataAquisicao = DataEscolhida();
        veiculo.Licenciado = chkLicenciado.IsChecked;
        veiculo.Observacoes = (edtObservacoes.Text ?? string.Empty).Trim();
    }
}
