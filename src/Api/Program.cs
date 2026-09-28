using BancoDistribuido.Api.Endpoints;
using BancoDistribuido.Api.Middleware;
using BancoDistribuido.Application;
using BancoDistribuido.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Los errores de binding (JSON mal formado, body faltante) se lanzan como
// BadHttpRequestException para que los atienda el middleware centralizado.
builder.Services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = true);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapCuentasEndpoints();
app.MapTransaccionesEndpoints();
app.MapClientesEndpoints();

app.Run();
