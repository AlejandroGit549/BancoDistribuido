using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Domain.Entities;
using MediatR;

namespace BancoDistribuido.Application.Features.Clientes.ObtenerEstadoFinanciero;

public sealed class ObtenerEstadoFinancieroQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ObtenerEstadoFinancieroQuery, EstadoFinanciero>
{
    public Task<EstadoFinanciero> Handle(ObtenerEstadoFinancieroQuery request, CancellationToken cancellationToken) =>
        unitOfWork.EstadosFinancieros.ObtenerAsync(request.IdCliente, cancellationToken);
}
