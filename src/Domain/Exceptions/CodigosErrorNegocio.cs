namespace BancoDistribuido.Domain.Exceptions;

/// <summary>SQLSTATE de negocio que lanzan los SPs de MovimientosBD.</summary>
public static class CodigosErrorNegocio
{
    public const string ParametroObligatorio   = "NB000";
    public const string ClienteNoExiste        = "NB001";
    public const string ClienteNoActivo        = "NB002";
    public const string CuentaNoExiste         = "NB003";
    public const string CuentaNoActiva         = "NB004";
    public const string SaldoInsuficiente      = "NB005";
    public const string MontoInvalido          = "NB006";
    public const string TipoInvalido           = "NB007";
    public const string IdempotenciaOtraCuenta = "NB008";
}
