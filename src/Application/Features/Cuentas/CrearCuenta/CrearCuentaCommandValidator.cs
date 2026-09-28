using BancoDistribuido.Domain.Catalogos;
using FluentValidation;

namespace BancoDistribuido.Application.Features.Cuentas.CrearCuenta;

public sealed class CrearCuentaCommandValidator : AbstractValidator<CrearCuentaCommand>
{
    public CrearCuentaCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.IdCliente)
            .GreaterThan(0).WithMessage("El idCliente debe ser mayor a 0.");

        RuleFor(x => x.TipoCuenta)
            .NotEmpty().WithMessage("TipoCuenta es obligatorio.")
            .Must(TipoCuentaCatalogo.EsValido)
            .WithMessage(_ => $"TipoCuenta inválido. Valores permitidos: {string.Join(", ", TipoCuentaCatalogo.Descripciones)}.");

        // El SP no recibe estado: la cuenta nace Activa. Solo se valida el valor recibido.
        RuleFor(x => x.EstadoCuenta)
            .NotEmpty().WithMessage("EstadoCuenta es obligatorio.")
            .Must(EstadoCuentaCatalogo.EsActiva)
            .WithMessage($"EstadoCuenta inválido. Una cuenta nueva solo puede crearse como '{EstadoCuentaCatalogo.Activa}'.");
    }
}
