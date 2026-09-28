using BancoDistribuido.Domain.Entities;

namespace BancoDistribuido.Application.Abstractions.Persistence;

/// <summary>MovimientosBD (PostgreSQL) - movimientos.sp_registrar_transaccion.</summary>
public interface ITransaccionRepository
{
    Task<Transaccion> RegistrarAsync(
        long idCuenta,
        string claveTipoTransaccion,
        decimal monto,
        string userIdInsert,
        Guid? claveIdempotencia,
        string? referencia,
        CancellationToken ct);
}
