using BancoDistribuido.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BancoDistribuido.Api.Middleware;

/// <summary>
/// Centraliza el manejo de errores y los devuelve como ProblemDetails (RFC 9457):
///  - ValidationException (pipeline de MediatR)  → 400 con el detalle por campo
///  - BadHttpRequestException (JSON mal formado) → 400
///  - NegocioException (SQLSTATE NB000..NB008)   → según la tabla de códigos
///  - Cualquier otro error                       → 500 (sin exponer detalles internos)
/// </summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private const string ContentType = "application/problem+json";

    private static readonly Dictionary<string, int> StatusPorCodigo = new()
    {
        [CodigosErrorNegocio.ParametroObligatorio]   = StatusCodes.Status400BadRequest,
        [CodigosErrorNegocio.ClienteNoExiste]        = StatusCodes.Status404NotFound,
        [CodigosErrorNegocio.ClienteNoActivo]        = StatusCodes.Status409Conflict,
        [CodigosErrorNegocio.CuentaNoExiste]         = StatusCodes.Status404NotFound,
        [CodigosErrorNegocio.CuentaNoActiva]         = StatusCodes.Status409Conflict,
        [CodigosErrorNegocio.SaldoInsuficiente]      = StatusCodes.Status422UnprocessableEntity,
        [CodigosErrorNegocio.MontoInvalido]          = StatusCodes.Status400BadRequest,
        [CodigosErrorNegocio.TipoInvalido]           = StatusCodes.Status400BadRequest,
        [CodigosErrorNegocio.IdempotenciaOtraCuenta] = StatusCodes.Status409Conflict
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) when (!context.Response.HasStarted)
        {
            await ManejarAsync(context, ex);
        }
    }

    private async Task ManejarAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case ValidationException ve:
            {
                var errores = ve.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Distinct().ToArray());

                var problem = new HttpValidationProblemDetails(errores)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title  = "La petición contiene errores de validación.",
                };
                await EscribirAsync(context, problem);
                break;
            }

            case BadHttpRequestException bre:
            {
                logger.LogWarning(bre, "Petición inválida: {Mensaje}", bre.Message);
                var problem = new ProblemDetails
                {
                    Status = bre.StatusCode,
                    Title  = "Petición inválida.",
                    Detail = "El JSON de entrada no es válido o falta un parámetro requerido.",
                };
                await EscribirAsync(context, problem);
                break;
            }

            case NegocioException ne:
            {
                var status = StatusPorCodigo.GetValueOrDefault(ne.Codigo, StatusCodes.Status400BadRequest);
                logger.LogWarning("Regla de negocio {Codigo}: {Mensaje}", ne.Codigo, ne.Message);

                var problem = new ProblemDetails
                {
                    Status = status,
                    Title  = ne.Message,
                    Detail = ne.Detalle,
                };
                problem.Extensions["codigo"] = ne.Codigo;
                await EscribirAsync(context, problem);
                break;
            }

            default:
            {
                logger.LogError(exception, "Error no controlado.");
                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title  = "Ocurrió un error interno. Intente más tarde.",
                };
                await EscribirAsync(context, problem);
                break;
            }
        }
    }

    private static Task EscribirAsync<T>(HttpContext context, T problem) where T : ProblemDetails
    {
        problem.Instance = context.Request.Path;
        problem.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsJsonAsync(problem, options: null, contentType: ContentType);
    }
}
