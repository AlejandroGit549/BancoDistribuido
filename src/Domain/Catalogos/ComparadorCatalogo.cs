using System.Globalization;

namespace BancoDistribuido.Domain.Catalogos;

/// <summary>Compara descripciones ignorando mayúsculas y acentos ("Nomina" = "Nómina").</summary>
internal static class ComparadorCatalogo
{
    public static readonly StringComparer Instancia = StringComparer.Create(
        CultureInfo.InvariantCulture,
        CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
}
