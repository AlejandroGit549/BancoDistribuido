using BancoDistribuido.Domain.Catalogos;
using FluentValidation;

namespace BancoDistribuido.Application.Features.Transacciones.RegistrarTransaccion;

public sealed class RegistrarTransaccionCommandValidator : AbstractValidator<RegistrarTransaccionCommand>
{
    public RegistrarTransaccionCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.IdCuenta)
            .GreaterThan(0).WithMessage("El idCuenta debe ser mayor a 0.");

        RuleFor(x => x.Monto)
            .NotNull().WithMessage("Monto es obligatorio.")
            .GreaterThan(0m).WithMessage("El monto debe ser mayor a 0.")
            .PrecisionScale(18, 2, ignoreTrailingZeros: true)
            .WithMessage("El monto admite como máximo 16 enteros y 2 decimales (NUMERIC(18,2)).");

        RuleFor(x => x.TipoTransaccion)
            .NotEmpty().WithMessage("TipoTransaccion es obligatorio.")
            .Must(TipoTransaccionCatalogo.EsValido)
            .WithMessage(_ => $"TipoTransaccion inválido. Valores permitidos: {string.Join(", ", TipoTransaccionCatalogo.Descripciones)}.");

        RuleFor(x => x.Referencia)
            .MaximumLength(50).WithMessage("La referencia admite como máximo 50 caracteres.");
    }
}
