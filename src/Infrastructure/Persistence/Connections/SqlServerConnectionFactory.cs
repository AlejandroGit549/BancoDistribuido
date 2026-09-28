using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace BancoDistribuido.Infrastructure.Persistence.Connections;

internal sealed class SqlServerConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public MotorBaseDatos Motor => MotorBaseDatos.SqlServer;

    public DbConnection CreateConnection() => new SqlConnection(connectionString);
}
