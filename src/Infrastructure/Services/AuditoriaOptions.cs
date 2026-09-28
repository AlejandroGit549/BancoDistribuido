namespace BancoDistribuido.Infrastructure.Services;

public sealed class AuditoriaOptions
{
    public const string Seccion = "Auditoria";

    public string UserIdInsert { get; init; } = string.Empty;
}
