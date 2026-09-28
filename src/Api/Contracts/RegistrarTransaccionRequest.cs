namespace BancoDistribuido.Api.Contracts;

/// <summary>{ "Monto": 500.00, "TipoTransaccion": "Retiro" }</summary>
public sealed record RegistrarTransaccionRequest(decimal? Monto, string? TipoTransaccion);
