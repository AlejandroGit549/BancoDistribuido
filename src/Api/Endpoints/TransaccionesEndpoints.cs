using BancoDistribuido.Api.Contracts;
using BancoDistribuido.Application.Features.Transacciones.RegistrarTransaccion;
using BancoDistribuido.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BancoDistribuido.Api.Endpoints;

public static class TransaccionesEndpoints
{
    public static IEndpointRouteBuilder MapTransaccionesEndpoints(this IEndpointRouteBuilder app)
    {
        // 2. Registrar una transacción (depósito / retiro)
        app.MapPost("/cuentas/{idCuenta:long}/transacciones", async (
                long idCuenta,
                RegistrarTransaccionRequest request,
                [FromHeader(Name = "Idempotency-Key")] Guid? claveIdempotencia,
                [FromHeader(Name = "X-Referencia")] string? referencia,
                ISender sender,
                CancellationToken ct) =>
            {
                var transaccion = await sender.Send(new RegistrarTransaccionCommand(
                    idCuenta,
                    request.Monto,
                    request.TipoTransaccion,
                    claveIdempotencia,
                    referencia), ct);

                return TypedResults.Created((string?)null, transaccion);
            })
            .WithName("RegistrarTransaccion")
            .WithTags("Transacciones")
            .Produces<Transaccion>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        return app;
    }
}
