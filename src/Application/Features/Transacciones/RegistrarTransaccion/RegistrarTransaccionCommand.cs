using BancoDistribuido.Domain.Entities;
using MediatR;

namespace BancoDistribuido.Application.Features.Transacciones.RegistrarTransaccion;

public sealed record RegistrarTransaccionCommand(
    long IdCuenta,
    decimal? Monto,
    string? TipoTransaccion,
    Guid? ClaveIdempotencia,
    string? Referencia) : IRequest<Transaccion>;
