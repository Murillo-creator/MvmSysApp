using SQLite;

namespace MvmSysApp.Models;

[Table("Modelos")]
public class Modelo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100), NotNull, Unique]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Categoria { get; set; } = string.Empty;
    
    public int Portas { get; set; } = 4;

    // Texto da segunda linha do item da lista (propriedade calculada)
    [Ignore]
    public string Detalhe => $"{Categoria} • {Portas} portas";
}
