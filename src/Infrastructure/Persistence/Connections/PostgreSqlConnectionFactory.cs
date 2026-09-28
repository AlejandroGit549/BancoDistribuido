using System.Data.Common;
using Npgsql;

namespace BancoDistribuido.Infrastructure.Persistence.Connections;

internal sealed class PostgreSqlConnectionFactory(string connectionString) : IDbConnectionFactory, IDisposable
{
    private readonly NpgsqlDataSource _dataSource = NpgsqlDataSource.Create(connectionString);

    public MotorBaseDatos Motor => MotorBaseDatos.PostgreSql;

    public DbConnection CreateConnection() => _dataSource.CreateConnection();

    public void Dispose() => _dataSource.Dispose();
}
