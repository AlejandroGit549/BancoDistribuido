using BancoDistribuido.Domain.Entities;

namespace BancoDistribuido.Application.Abstractions.Persistence;

/// <summary>MovimientosBD (PostgreSQL) - movimientos.sp_crear_cuenta.</summary>
public interface ICuentaRepository
{
    Task<Cuenta> CrearAsync(int idCliente, string claveTipoCuenta, string userIdInsert, CancellationToken ct);
}
