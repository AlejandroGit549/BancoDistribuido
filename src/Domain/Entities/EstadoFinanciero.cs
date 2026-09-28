using System.Text.Json.Serialization;

namespace BancoDistribuido.Domain.Entities;

/// <summary>Respuesta de movimientos.sp_obtener_estado_financiero.</summary>
public sealed class EstadoFinanciero
{
    [JsonPropertyName("ID_Cliente")]    public int IdCliente { get; init; }
    [JsonPropertyName("Nombre")]        public string Nombre { get; init; } = string.Empty;
    [JsonPropertyName("Estado")]        public string Estado { get; init; } = string.Empty;
    [JsonPropertyName("Cuentas")]       public List<CuentaResumen> Cuentas { get; init; } = [];
    [JsonPropertyName("Transacciones")] public List<TransaccionResumen> Transacciones { get; init; } = [];
}

public sealed class CuentaResumen
{
    [JsonPropertyName("ID_Cuenta")]    public long IdCuenta { get; init; }
    [JsonPropertyName("NumeroCuenta")] public string NumeroCuenta { get; init; } = string.Empty;
    [JsonPropertyName("Saldo")]        public decimal Saldo { get; init; }
}

public sealed class TransaccionResumen
{
    [JsonPropertyName("ID_Transaccion")]  public long IdTransaccion { get; init; }
    [JsonPropertyName("Monto")]           public decimal Monto { get; init; }
    [JsonPropertyName("Fecha")]           public DateTime Fecha { get; init; }
    [JsonPropertyName("TipoTransaccion")] public string TipoTransaccion { get; init; } = string.Empty;
}
