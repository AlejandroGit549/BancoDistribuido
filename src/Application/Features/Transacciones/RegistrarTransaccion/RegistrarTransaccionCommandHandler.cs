using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Application.Abstractions.Services;
using BancoDistribuido.Domain.Catalogos;
using BancoDistribuido.Domain.Entities;
using BancoDistribuido.Domain.Exceptions;
using MediatR;

namespace BancoDistribuido.Application.Features.Transacciones.RegistrarTransaccion;

public sealed class RegistrarTransaccionCommandHandler(IUnitOfWork unitOfWork, IUsuarioContexto usuario)
    : IRequestHandler<RegistrarTransaccionCommand, Transaccion>
{
    public async Task<Transaccion> Handle(RegistrarTransaccionCommand request, CancellationToken cancellationToken)
    {
        if (!TipoTransaccionCatalogo.TryObtenerClave(request.TipoTransaccion, out var claveTipo))
            throw new NegocioException(CodigosErrorNegocio.TipoInvalido,
                $"Tipo de transacción inválido: {request.TipoTransaccion}.");

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {

            var _validate = await unitOfWork.Clientes.ValidateClienteId(2, cancellationToken);

            if (_validate == null)
                throw new NegocioException(CodigosErrorNegocio.ClienteNoExiste,
                    $"El cliente con Id {2} no existe.");

            var transaccion = await unitOfWork.Transacciones.RegistrarAsync(
                request.IdCuenta,
                claveTipo,
                request.Monto!.Value,
                usuario.UserId,
                request.ClaveIdempotencia,
                request.Referencia,
                cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
            return transaccion;
        }
        catch
        {
            await unitOfWork.RollbackAsync(CancellationToken.None);
            throw;
        }
    }
}
