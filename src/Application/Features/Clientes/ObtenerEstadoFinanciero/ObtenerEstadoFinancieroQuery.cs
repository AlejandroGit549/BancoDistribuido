using BancoDistribuido.Domain.Entities;
using MediatR;

namespace BancoDistribuido.Application.Features.Clientes.ObtenerEstadoFinanciero;

public sealed record ObtenerEstadoFinancieroQuery(int IdCliente) : IRequest<EstadoFinanciero>;
