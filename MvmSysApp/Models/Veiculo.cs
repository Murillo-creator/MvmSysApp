using SQLite;

namespace MvmSysApp.Models;

// Classe de modelo (POCO): representa um registro de veículo.
[Table("Veiculos")]
public class Veiculo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // = string.Empty evita null (o XAML faz Binding direto nessas propriedades)
    [Unique, NotNull]
    public string Placa { get; set; } = string.Empty;
    [Unique, NotNull]
    public string Modelo { get; set; } = string.Empty;
    [NotNull]
    public string Cor { get; set; } = string.Empty;
    [NotNull]
    public int Ano { get; set; }
    public DateTime DataAquisicao { get; set; } = DateTime.Today;
    public bool Licenciado { get; set; }
    public string Observacoes { get; set; } = string.Empty;

    // Texto usado na segunda linha do item da lista.
    // Propriedade calculada (somente get): montada a partir das outras.
    [Ignore]
    public string Detalhe => $"{Modelo} • {Cor} • {Ano}";

    // Terceira linha do item: data de aquisição e situação do licenciamento.
    [Ignore]
    public string Situacao => $"Adquirido em {DataAquisicao:d} • {(Licenciado ? "Licenciado" : "Não licenciado")}";
}
