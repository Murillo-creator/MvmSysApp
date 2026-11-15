namespace MvmSysApp.Models;

/// <summary>Toda entidade do CRUD precisa de um Id.</summary>
public interface IEntidade
{
    int Id { get; set; }
}
