namespace BancoDistribuido.Application.Abstractions.Persistence;

/// <summary>
/// Centraliza el acceso a los repositorios de ambas bases de datos.
/// La transacción aplica a MovimientosBD (PostgreSQL), donde ocurren todas las escrituras;
/// no existe transacción distribuida entre motores.
/// </summary>
public interface IUnitOfWork
{
    ICuentaRepository Cuentas { get; }
    ITransaccionRepository Transacciones { get; }
    IEstadoFinancieroRepository EstadosFinancieros { get; }
    IClienteRepository Clientes { get; }

    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
    Task RollbackAsync(CancellationToken ct = default);
}
