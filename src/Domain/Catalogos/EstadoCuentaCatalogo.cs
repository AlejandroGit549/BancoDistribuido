namespace BancoDistribuido.Domain.Catalogos;

/// <summary>movimientos.cat_estado_cuenta. Al crear, la cuenta solo puede nacer "Activa" (DEFAULT del SP).</summary>
public static class EstadoCuentaCatalogo
{
    public const string Activa = "Activa";

    public static bool EsActiva(string? descripcion) =>
        !string.IsNullOrWhiteSpace(descripcion) &&
        ComparadorCatalogo.Instancia.Equals(descripcion.Trim(), Activa);
}
