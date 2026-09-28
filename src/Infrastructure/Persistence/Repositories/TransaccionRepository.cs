using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Domain.Entities;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace BancoDistribuido.Infrastructure.Persistence.Repositories;

internal sealed class TransaccionRepository(
    [FromKeyedServices(MotorBaseDatos.PostgreSql)] DbSession session) : ITransaccionRepository
{
    private const string Sql =
        """
        CALL movimientos.sp_registrar_transaccion(
            @p_id_cuenta,
            @p_clave_tipo_transaccion::varchar,
            @p_monto,
            @p_user_id_insert::varchar,
            @p_clave_idempotencia::uuid,
            @p_referencia::varchar,
            NULL::json)
        """;

    public Task<Transaccion> RegistrarAsync(
        long idCuenta,
        string claveTipoTransaccion,
        decimal monto,
        string userIdInsert,
        Guid? claveIdempotencia,
        string? referencia,
        CancellationToken ct) =>
        StoredProcedureExecutor.CallJsonAsync<Transaccion>(session, Sql, new
        {
            p_id_cuenta              = idCuenta,
            p_clave_tipo_transaccion = claveTipoTransaccion,
            p_monto                  = monto,
            p_user_id_insert         = userIdInsert,
            p_clave_idempotencia     = claveIdempotencia,
            p_referencia             = referencia
        }, ct);
}
