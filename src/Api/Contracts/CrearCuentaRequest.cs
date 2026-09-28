namespace BancoDistribuido.Api.Contracts;

/// <summary>{ "TipoCuenta": "Ahorro", "EstadoCuenta": "Activa" }</summary>
public sealed record CrearCuentaRequest(string? TipoCuenta, string? EstadoCuenta);
