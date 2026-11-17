using MvmSysApp.Models;

namespace MvmSysApp.Data;

public class ModeloRepository(AppDatabase database)
{
    public async Task<List<Modelo>> ListarAsync()
    {
        var db = await database.GetConnectionAsync();

        return await db.Table<Modelo>()
            .OrderBy(m => m.Nome)
            .ToListAsync();
    }

    public async Task<Modelo?> ObterAsync(int id)
    {
        var db = await database.GetConnectionAsync();
        return await db.FindAsync<Modelo>(id);
    }

    public async Task SalvarAsync(Modelo modelo)
    {
        var db = await database.GetConnectionAsync();

        if (modelo.Id == 0)
            await db.InsertAsync(modelo);
        else
            await db.UpdateAsync(modelo);
    }

    public async Task ExcluirAsync(int id)
    {
        var db = await database.GetConnectionAsync();
        await db.DeleteAsync<Modelo>(id);
    }
}