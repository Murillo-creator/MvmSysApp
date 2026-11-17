using SQLite;
using MvmSysApp.Models;

namespace MvmSysApp.Data;

public class AppDatabase
{
    private readonly SQLiteAsyncConnection _connection;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private bool _initialized;

    public AppDatabase()
    {
        var caminho = Path.Combine(
            FileSystem.AppDataDirectory,
            "mvmsysapp.db3");

        _connection = new SQLiteAsyncConnection(caminho);
    }

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_initialized)
            return _connection;

        await _gate.WaitAsync();

        try
        {
            if (!_initialized)
            {
                await _connection.CreateTableAsync<Marca>();
                await _connection.CreateTableAsync<Modelo>();
                await _connection.CreateTableAsync<Veiculo>();

                _initialized = true;
            }
        }
        finally
        {
            _gate.Release();
        }

        return _connection;
    }
}
