using SQLite;

namespace MvmSysApp.Models;

[Table("Marcas")]
public class Marca
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100), NotNull, Unique]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Pais { get; set; } = string.Empty;

    public bool Ativa { get; set; } = true;

    // Texto da segunda linha do item da lista (propriedade calculada)
    [Ignore]
    public string Detalhe => $"{Pais} • {(Ativa ? "Ativa" : "Inativa")}";
}
