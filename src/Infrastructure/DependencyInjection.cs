using BancoDistribuido.Application.Abstractions.Persistence;
using BancoDistribuido.Application.Abstractions.Services;
using BancoDistribuido.Infrastructure.Persistence;
using BancoDistribuido.Infrastructure.Persistence.Connections;
using BancoDistribuido.Infrastructure.Persistence.Repositories;
using BancoDistribuido.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BancoDistribuido.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var movimientosBd = configuration.GetConnectionString("MovimientosBD")
            ?? throw new InvalidOperationException("Falta la cadena de conexión 'MovimientosBD' (PostgreSQL).");
        var clientesBd = configuration.GetConnectionString("ClientesBD")
            ?? throw new InvalidOperationException("Falta la cadena de conexión 'ClientesBD' (SQL Server).");

        // Conexiones distribuidas: una fábrica por motor
        // (registradas con factory para que el contenedor libere el NpgsqlDataSource al apagar)
        services.AddKeyedSingleton<IDbConnectionFactory>(MotorBaseDatos.PostgreSql,
            (_, _) => new PostgreSqlConnectionFactory(movimientosBd));
        services.AddKeyedSingleton<IDbConnectionFactory>(MotorBaseDatos.SqlServer,
            (_, _) => new SqlServerConnectionFactory(clientesBd));

        // Una sesión (conexión + transacción) por motor y por request
        services.AddKeyedScoped<DbSession>(MotorBaseDatos.PostgreSql,
            (sp, key) => new DbSession(sp.GetRequiredKeyedService<IDbConnectionFactory>(key)));
        services.AddKeyedScoped<DbSession>(MotorBaseDatos.SqlServer,
            (sp, key) => new DbSession(sp.GetRequiredKeyedService<IDbConnectionFactory>(key)));

        // Repositorios
        services.AddScoped<ICuentaRepository, CuentaRepository>();
        services.AddScoped<ITransaccionRepository, TransaccionRepository>();
        services.AddScoped<IEstadoFinancieroRepository, EstadoFinancieroRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Auditoría
        services.Configure<AuditoriaOptions>(configuration.GetSection(AuditoriaOptions.Seccion));
        services.AddSingleton<IUsuarioContexto, UsuarioContexto>();

        return services;
    }
}
