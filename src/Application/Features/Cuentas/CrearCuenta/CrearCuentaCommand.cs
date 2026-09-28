using BancoDistribuido.Domain.Entities;
using MediatR;

namespace BancoDistribuido.Application.Features.Cuentas.CrearCuenta;

public sealed record CrearCuentaCommand(int IdCliente, string? TipoCuenta, string? EstadoCuenta)
    : IRequest<Cuenta>;
