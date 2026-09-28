using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace BancoDistribuido.Infrastructure.Persistence;

internal sealed class UnitOfWork(
    ICuentaRepository cuentas,
    ITransaccionRepository transacciones,
    IEstadoFinancieroRepository estadosFinancieros,
    IClienteRepository clientes,
    [FromKeyedServices(MotorBaseDatos.PostgreSql)] DbSession movimientos) : IUnitOfWork
{
    public ICuentaRepository Cuentas => cuentas;
    public ITransaccionRepository Transacciones => transacciones;
    public IEstadoFinancieroRepository EstadosFinancieros => estadosFinancieros;
    public IClienteRepository Clientes => clientes;

    public Task BeginTransactionAsync(CancellationToken ct = default) => movimientos.BeginTransactionAsync(ct);
    public Task CommitAsync(CancellationToken ct = default) => movimientos.CommitAsync(ct);
    public Task RollbackAsync(CancellationToken ct = default) => movimientos.RollbackAsync(ct);
}
