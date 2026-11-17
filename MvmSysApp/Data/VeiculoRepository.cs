using MvmSysApp.Models;

namespace MvmSysApp.Data;

public class VeiculoRepository(AppDatabase database)
{
    public async Task<List<Veiculo>> ListarAsync()
    {
        var db = await database.GetConnectionAsync();

        return await db.Table<Veiculo>()
            .OrderBy(v => v.Placa)
            .ToListAsync();
    }

    public async Task<Veiculo?> ObterAsync(int id)
    {
        var db = await database.GetConnectionAsync();
        return await db.FindAsync<Veiculo>(id);
    }

    public async Task SalvarAsync(Veiculo veiculo)
    {
        var db = await database.GetConnectionAsync();

        if (veiculo.Id == 0)
            await db.InsertAsync(veiculo);
        else
            await db.UpdateAsync(veiculo);
    }

    public async Task ExcluirAsync(int id)
    {
        var db = await database.GetConnectionAsync();
        await db.DeleteAsync<Veiculo>(id);
    }
}