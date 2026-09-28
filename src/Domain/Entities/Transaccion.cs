using System.Text.Json.Serialization;

namespace BancoDistribuido.Domain.Entities;

/// <summary>Respuesta de movimientos.sp_registrar_transaccion.</summary>
public sealed class Transaccion
{
    [JsonPropertyName("ID_Transaccion")]    public long IdTransaccion { get; init; }
    [JsonPropertyName("ID_Cuenta")]         public long IdCuenta { get; init; }
    [JsonPropertyName("Monto")]             public decimal Monto { get; init; }
    [JsonPropertyName("Fecha")]             public DateTime Fecha { get; init; }
    [JsonPropertyName("TipoTransaccion")]   public string TipoTransaccion { get; init; } = string.Empty;
    [JsonPropertyName("EstadoTransaccion")] public string EstadoTransaccion { get; init; } = string.Empty;
}
