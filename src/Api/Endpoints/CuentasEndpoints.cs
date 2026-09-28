using BancoDistribuido.Api.Contracts;
using BancoDistribuido.Application.Features.Cuentas.CrearCuenta;
using BancoDistribuido.Domain.Entities;
using MediatR;

namespace BancoDistribuido.Api.Endpoints;

public static class CuentasEndpoints
{
    public static IEndpointRouteBuilder MapCuentasEndpoints(this IEndpointRouteBuilder app)
    {
        // 1. Crear cuenta bancaria para un cliente existente
        app.MapPost("/clientes/{idCliente:int}/cuentas", async (
                int idCliente,
                CrearCuentaRequest request,
                ISender sender,
                CancellationToken ct) =>
            {
                var cuenta = await sender.Send(
                    new CrearCuentaCommand(idCliente, request.TipoCuenta, request.EstadoCuenta), ct);

                return TypedResults.Created((string?)null, cuenta);
            })
            .WithName("CrearCuenta")
            .WithTags("Cuentas")
            .Produces<Cuenta>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        return app;
    }
}
