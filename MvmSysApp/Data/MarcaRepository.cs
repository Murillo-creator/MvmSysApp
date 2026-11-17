using MvmSysApp.Models;

namespace MvmSysApp.Data;

public class MarcaRepository(AppDatabase database)
{
    public async Task<List<Marca>> ListarAsync()
    {
        var db = await database.GetConnectionAsync();

        return await db.Table<Marca>()
            .OrderBy(m => m.Nome)
            .ToListAsync();
    }

    public async Task<Marca?> ObterAsync(int id)
    {
        var db = await database.GetConnectionAsync();
        return await db.FindAsync<Marca>(id);
    }

    public async Task SalvarAsync(Marca marca)
    {
        var db = await database.GetConnectionAsync();

        if (marca.Id == 0)
            await db.InsertAsync(marca);
        else
            await db.UpdateAsync(marca);
    }

    public async Task ExcluirAsync(int id)
    {
        var db = await database.GetConnectionAsync();
        await db.DeleteAsync<Marca>(id);
    }
}