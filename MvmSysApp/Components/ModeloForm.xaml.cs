using MvmSysApp.Models;

namespace MvmSysApp.Components;

public partial class ModeloForm : ContentView
{
    public ModeloForm()
    {
        InitializeComponent();
    }

    // Converte o RadioButton marcado no texto da categoria ("" se nenhum)
    private string CategoriaSelecionada()
    {
        if (rbSedan.IsChecked) return "Sedan";
        if (rbHatch.IsChecked) return "Hatch";
        if (rbSuv.IsChecked) return "SUV";
        if (rbPicape.IsChecked) return "Picape";
        return string.Empty;
    }

    // Mantém o texto "Número de portas: N" sempre igual ao valor do Stepper
    private void AtualizarTextoPortas()
    {
        lblPortas.Text = $"Número de portas: {(int)stpPortas.Value}";
    }

    private void stpPortasOnValueChanged(object? sender, ValueChangedEventArgs e)
    {
        AtualizarTextoPortas();
    }

    public void Preencher(Modelo modelo)
    {
        entNome.Text = modelo.Nome;
        rbSedan.IsChecked = modelo.Categoria == "Sedan";
        rbHatch.IsChecked = modelo.Categoria == "Hatch";
        rbSuv.IsChecked = modelo.Categoria == "SUV";
        rbPicape.IsChecked = modelo.Categoria == "Picape";
        stpPortas.Value = Math.Clamp(modelo.Portas, 2, 5);
        AtualizarTextoPortas();
    }

    public bool Validar(out string mensagem)
    {
        if (string.IsNullOrWhiteSpace(entNome.Text))
        {
            mensagem = "Informe o nome do modelo.";
            return false;
        }

        if (CategoriaSelecionada().Length == 0)
        {
            mensagem = "Selecione a categoria.";
            return false;
        }

        mensagem = string.Empty;
        return true;
    }

    public void CopiarPara(Modelo modelo)
    {
        modelo.Nome = (entNome.Text ?? string.Empty).Trim();
        modelo.Categoria = CategoriaSelecionada();
        modelo.Portas = (int)stpPortas.Value;
    }
}
