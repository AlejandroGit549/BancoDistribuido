namespace BancoDistribuido.Domain.Catalogos;

/// <summary>movimientos.cat_tipo_transaccion (solo naturaleza A/C; APERTURA no se expone).</summary>
public static class TipoTransaccionCatalogo
{
    private static readonly Dictionary<string, string> Mapa = new(ComparadorCatalogo.Instancia)
    {
        ["Depósito"] = "DEPOSITO",
        ["Retiro"]   = "RETIRO"
    };

    public static IEnumerable<string> Descripciones => Mapa.Keys;

    public static bool EsValido(string? descripcion) =>
        !string.IsNullOrWhiteSpace(descripcion) && Mapa.ContainsKey(descripcion.Trim());

    public static bool TryObtenerClave(string? descripcion, out string clave)
    {
        clave = string.Empty;
        if (string.IsNullOrWhiteSpace(descripcion) || !Mapa.TryGetValue(descripcion.Trim(), out var valor))
            return false;
        clave = valor;
        return true;
    }
}
