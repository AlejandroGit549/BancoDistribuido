using BancoDistribuido.Application.Features.Clientes.ObtenerEstadoFinanciero;
using BancoDistribuido.Domain.Entities;
using MediatR;

namespace BancoDistribuido.Api.Endpoints;

public static class ClientesEndpoints
{
    public static IEndpointRouteBuilder MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        // 3. Estado financiero del cliente
        app.MapGet("/clientes/{idCliente:int}/estado-financiero", async (
                int idCliente,
                ISender sender,
                CancellationToken ct) =>
            {
                var estado = await sender.Send(new ObtenerEstadoFinancieroQuery(idCliente), ct);
                return TypedResults.Ok(estado);
            })
            .WithName("ObtenerEstadoFinanciero")
            .WithTags("Clientes")
            .Produces<EstadoFinanciero>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}
