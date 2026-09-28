using System.Text.Json.Serialization;

namespace BancoDistribuido.Domain.Entities;

/// <summary>Respuesta de movimientos.sp_crear_cuenta (el orden de las propiedades respeta el contrato).</summary>
public sealed class Cuenta
{
    [JsonPropertyName("ID_Cuenta")]    public long IdCuenta { get; init; }
    [JsonPropertyName("ID_Cliente")]   public int IdCliente { get; init; }
    [JsonPropertyName("NumeroCuenta")] public string NumeroCuenta { get; init; } = string.Empty;
    [JsonPropertyName("Saldo")]        public decimal Saldo { get; init; }
    [JsonPropertyName("TipoCuenta")]   public string TipoCuenta { get; init; } = string.Empty;
    [JsonPropertyName("EstadoCuenta")] public string EstadoCuenta { get; init; } = string.Empty;
}
