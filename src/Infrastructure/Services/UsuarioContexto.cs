using BancoDistribuido.Application.Abstractions.Services;
using Microsoft.Extensions.Options;

namespace BancoDistribuido.Infrastructure.Services;

internal sealed class UsuarioContexto(IOptions<AuditoriaOptions> options) : IUsuarioContexto
{
    public string UserId { get; } = string.IsNullOrWhiteSpace(options.Value.UserIdInsert)
        ? throw new InvalidOperationException("Configure 'Auditoria:UserIdInsert' en appsettings.")
        : options.Value.UserIdInsert;
}
