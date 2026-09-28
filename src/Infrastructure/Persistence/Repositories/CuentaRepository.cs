using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Domain.Entities;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace BancoDistribuido.Infrastructure.Persistence.Repositories;

internal sealed class CuentaRepository(
    [FromKeyedServices(MotorBaseDatos.PostgreSql)] DbSession session) : ICuentaRepository
{
    private const string Sql =
        "CALL movimientos.sp_crear_cuenta(@p_id_cliente, @p_clave_tipo_cuenta::varchar, @p_user_id_insert::varchar, NULL::json)";

    public Task<Cuenta> CrearAsync(int idCliente, string claveTipoCuenta, string userIdInsert, CancellationToken ct) =>
        StoredProcedureExecutor.CallJsonAsync<Cuenta>(session, Sql, new
        {
            p_id_cliente        = idCliente,
            p_clave_tipo_cuenta = claveTipoCuenta,
            p_user_id_insert    = userIdInsert
        }, ct);
}
