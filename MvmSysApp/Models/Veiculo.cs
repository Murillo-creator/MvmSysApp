namespace MvmSysApp.Models;

// Classe de modelo (POCO): representa um registro de veículo.
// Implementa IEntidade para garantir o Id exigido pelo repositório genérico.
public class Veiculo : IEntidade
{
    public int Id { get; set; }
    // = string.Empty evita null (o XAML faz Binding direto nessas propriedades)
    public string Placa { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public int Ano { get; set; }
    public DateTime DataAquisicao { get; set; } = DateTime.Today;
    public bool Licenciado { get; set; }
    public string Observacoes { get; set; } = string.Empty;

    // Texto usado na segunda linha do item da lista.
    // Propriedade calculada (somente get): montada a partir das outras.
    public string Detalhe => $"{Modelo} • {Cor} • {Ano}";

    // Terceira linha do item: data de aquisição e situação do licenciamento.
    public string Situacao => $"Adquirido em {DataAquisicao:d} • {(Licenciado ? "Licenciado" : "Não licenciado")}";
}
