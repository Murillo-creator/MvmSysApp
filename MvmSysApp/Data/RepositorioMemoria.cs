using MvmSysApp.Models;

namespace MvmSysApp.Data;

/// <summary>
/// Repositório genérico que guarda os dados em memória (List).
/// Os dados somem quando o app fecha. Para persistir a implementação
/// será trocada por SQLite mantendo os mesmos métodos.
/// </summary>
public class RepositorioMemoria<T> where T : class, IEntidade
{
    private readonly List<T> _itens = new();
    private int _proximoId = 1;

    public List<T> Listar() => _itens.ToList();

    public T? Obter(int id) => _itens.FirstOrDefault(i => i.Id == id);

    public void Inserir(T item)
    {
        item.Id = _proximoId++;
        _itens.Add(item);
    }

    public void Alterar(T item)
    {
        int indice = _itens.FindIndex(i => i.Id == item.Id);
        if (indice >= 0)
            _itens[indice] = item;
    }

    public void Excluir(int id)
    {
        _itens.RemoveAll(i => i.Id == id);
    }
}
