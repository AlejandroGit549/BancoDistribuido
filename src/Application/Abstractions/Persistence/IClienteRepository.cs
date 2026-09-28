using BancoDistribuido.Domain.Entities;

namespace BancoDistribuido.Application.Abstractions.Persistence;

/// <summary>ClientesBD (SQL Server) - dbo.vw_ClienteEstado. Disponible, no se usa aún en los endpoints.</summary>
public interface IClienteRepository
{
    Task<ClienteEstado?> ObtenerEstadoAsync(int idCliente, CancellationToken ct);
    Task<Cuenta?> ValidateClienteId(int idCliente, CancellationToken ct);
}
