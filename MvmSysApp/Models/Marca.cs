namespace MvmSysApp.Models;

public class Marca : IEntidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public bool Ativa { get; set; } = true;

    // Texto da segunda linha do item da lista (propriedade calculada)
    public string Detalhe => $"{Pais} • {(Ativa ? "Ativa" : "Inativa")}";
}
