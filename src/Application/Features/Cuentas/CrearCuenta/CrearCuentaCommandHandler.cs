using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Application.Abstractions.Services;
using BancoDistribuido.Domain.Catalogos;
using BancoDistribuido.Domain.Entities;
using BancoDistribuido.Domain.Exceptions;
using MediatR;

namespace BancoDistribuido.Application.Features.Cuentas.CrearCuenta;

public sealed class CrearCuentaCommandHandler(IUnitOfWork unitOfWork, IUsuarioContexto usuario)
    : IRequestHandler<CrearCuentaCommand, Cuenta>
{
    public async Task<Cuenta> Handle(CrearCuentaCommand request, CancellationToken cancellationToken)
    {
        if (!TipoCuentaCatalogo.TryObtenerClave(request.TipoCuenta, out var claveTipoCuenta))
            throw new NegocioException(CodigosErrorNegocio.TipoInvalido,
                $"Tipo de cuenta inválido: {request.TipoCuenta}.");

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var _validate = await unitOfWork.Clientes.ValidateClienteId(request.IdCliente, cancellationToken);

            if (_validate == null)
                throw new NegocioException(CodigosErrorNegocio.ClienteNoExiste,
                    $"El cliente con Id {request.IdCliente} no existe.");

            var cuenta = await unitOfWork.Cuentas.CrearAsync(
                request.IdCliente, claveTipoCuenta, usuario.UserId, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
            return cuenta;
        }
        catch
        {
            await unitOfWork.RollbackAsync(CancellationToken.None);
            throw;
        }
    }
}
