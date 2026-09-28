using System.Data;
using System.Data.Common;

namespace BancoDistribuido.Infrastructure.Persistence.Connections;

/// <summary>
/// Conexión (y transacción opcional) compartida por request para un motor.
/// Se abre en forma diferida: si un request no toca un motor, no se conecta a él.
/// </summary>
internal sealed class DbSession(IDbConnectionFactory factory) : IAsyncDisposable
{
    private DbConnection? _connection;

    public DbTransaction? Transaction { get; private set; }

    public async Task<DbConnection> GetConnectionAsync(CancellationToken ct)
    {
        _connection ??= factory.CreateConnection();

        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync(ct);

        return _connection;
    }

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        if (Transaction is not null)
            return;

        var connection = await GetConnectionAsync(ct);
        Transaction = await connection.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        if (Transaction is null)
            throw new InvalidOperationException("No hay una transacción activa para confirmar.");

        await Transaction.CommitAsync(ct);
        await Transaction.DisposeAsync();
        Transaction = null;
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (Transaction is null)
            return;

        await Transaction.RollbackAsync(ct);
        await Transaction.DisposeAsync();
        Transaction = null;
    }

    public async ValueTask DisposeAsync()
    {
        if (Transaction is not null)
            await Transaction.DisposeAsync();

        if (_connection is not null)
            await _connection.DisposeAsync();
    }
}
