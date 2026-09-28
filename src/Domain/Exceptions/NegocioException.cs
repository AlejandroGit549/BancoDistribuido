namespace BancoDistribuido.Domain.Exceptions;

/// <summary>Error de regla de negocio (NB000..NB008).</summary>
public sealed class NegocioException(string codigo, string mensaje, string? detalle = null)
    : Exception(mensaje)
{
    public string Codigo { get; } = codigo;
    public string? Detalle { get; } = detalle;
}
