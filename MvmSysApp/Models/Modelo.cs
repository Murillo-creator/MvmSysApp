namespace MvmSysApp.Models;

public class Modelo : IEntidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int Portas { get; set; } = 4;

    // Texto da segunda linha do item da lista (propriedade calculada)
    public string Detalhe => $"{Categoria} • {Portas} portas";
}
