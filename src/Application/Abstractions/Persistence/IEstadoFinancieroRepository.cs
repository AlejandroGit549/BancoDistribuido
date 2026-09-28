using BancoDistribuido.Domain.Entities;

namespace BancoDistribuido.Application.Abstractions.Persistence;

/// <summary>MovimientosBD (PostgreSQL) - movimientos.sp_obtener_estado_financiero.</summary>
public interface IEstadoFinancieroRepository
{
    Task<EstadoFinanciero> ObtenerAsync(int idCliente, CancellationToken ct);
}
