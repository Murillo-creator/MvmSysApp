using MvmSysApp.Models;

namespace MvmSysApp.Data;

/// <summary>
/// Ponto único de acesso aos dados. Como é estático, qualquer tela
/// enxerga as mesmas listas (Inserir, Alterar e Principal compartilham os dados).
/// </summary>
public static class Repositorios
{
    public static RepositorioMemoria<Marca> Marcas { get; } = new();
    public static RepositorioMemoria<Modelo> Modelos { get; } = new();
    public static RepositorioMemoria<Veiculo> Veiculos { get; } = new();
}
