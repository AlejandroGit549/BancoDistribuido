using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Domain.Entities;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace BancoDistribuido.Infrastructure.Persistence.Repositories;

internal sealed class EstadoFinancieroRepository(
    [FromKeyedServices(MotorBaseDatos.PostgreSql)] DbSession session) : IEstadoFinancieroRepository
{
    private const string Sql =
        "CALL movimientos.sp_obtener_estado_financiero(@p_id_cliente, NULL::json)";

    public Task<EstadoFinanciero> ObtenerAsync(int idCliente, CancellationToken ct) =>
        StoredProcedureExecutor.CallJsonAsync<EstadoFinanciero>(session, Sql, new
        {
            p_id_cliente = idCliente
        }, ct);
}
