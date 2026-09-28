namespace BancoDistribuido.Domain.Entities;

/// <summary>Registro de dbo.vw_ClienteEstado (ClientesBD - SQL Server).</summary>
public sealed class ClienteEstado
{
    public int IdCliente { get; init; }
    public string NombreCompleto { get; init; } = string.Empty;
    public string EstadoClave { get; init; } = string.Empty;
    public string EstadoDescripcion { get; init; } = string.Empty;
}
