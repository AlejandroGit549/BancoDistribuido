using FluentValidation;

namespace BancoDistribuido.Application.Features.Clientes.ObtenerEstadoFinanciero;

public sealed class ObtenerEstadoFinancieroQueryValidator : AbstractValidator<ObtenerEstadoFinancieroQuery>
{
    public ObtenerEstadoFinancieroQueryValidator()
    {
        RuleFor(x => x.IdCliente)
            .GreaterThan(0).WithMessage("El idCliente debe ser mayor a 0.");
    }
}
