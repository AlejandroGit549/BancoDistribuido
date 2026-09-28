using System.Text.Json;
using BancoDistribuido.Domain.Exceptions;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using Dapper;
using Npgsql;

namespace BancoDistribuido.Infrastructure.Persistence.Repositories;

/// <summary>
/// Ejecuta un procedimiento de PostgreSQL con parámetro INOUT p_respuesta JSON mediante Dapper,
/// deserializa la respuesta y traduce los SQLSTATE de negocio (NBxxx) a NegocioException.
/// </summary>
internal static class StoredProcedureExecutor
{
    private const string PrefijoErrorNegocio = "NB";

    public static async Task<T> CallJsonAsync<T>(DbSession session, string sql, object parametros, CancellationToken ct)
    {
        var connection = await session.GetConnectionAsync(ct);

        string? json;
        try
        {
            json = await connection.QuerySingleAsync<string?>(
                new CommandDefinition(sql, parametros, session.Transaction, cancellationToken: ct));
        }
        catch (PostgresException ex) when (ex.SqlState.StartsWith(PrefijoErrorNegocio, StringComparison.Ordinal))
        {
            throw new NegocioException(ex.SqlState, ex.MessageText, ex.Detail);
        }

        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidOperationException("El procedimiento no devolvió respuesta (p_respuesta es NULL).");

        return JsonSerializer.Deserialize<T>(json)
               ?? throw new InvalidOperationException("No fue posible interpretar la respuesta del procedimiento.");
    }
}
