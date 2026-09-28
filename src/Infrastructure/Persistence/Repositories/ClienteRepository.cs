using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Domain.Entities;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace BancoDistribuido.Infrastructure.Persistence.Repositories;

/// <summary>ClientesBD (SQL Server). Queda disponible; los endpoints actuales no lo utilizan.</summary>
internal sealed class ClienteRepository(
    [FromKeyedServices(MotorBaseDatos.SqlServer)] DbSession session) : IClienteRepository
{
    private const string Sql =
        """
        SELECT IdCliente, NombreCompleto, EstadoClave, EstadoDescripcion
        FROM dbo.vw_ClienteEstado
        WHERE IdCliente = @IdCliente;
        """;

    public async Task<ClienteEstado?> ObtenerEstadoAsync(int idCliente, CancellationToken ct)
    {
        var connection = await session.GetConnectionAsync(ct);

        return await connection.QuerySingleOrDefaultAsync<ClienteEstado>(
            new CommandDefinition(Sql, new { IdCliente = idCliente }, session.Transaction, cancellationToken: ct));
    }

    public async Task<Cuenta?> ValidateClienteId(int idCliente, CancellationToken ct)
    {
        var connection = await session.GetConnectionAsync(ct);

        return await connection.QuerySingleOrDefaultAsync<Cuenta>(
            new CommandDefinition("ValidateActiveUser", new { ClienteId = idCliente }, session.Transaction, cancellationToken: ct));
    }
}
