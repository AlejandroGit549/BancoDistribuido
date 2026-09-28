using System.Data.Common;

namespace BancoDistribuido.Infrastructure.Persistence.Connections;

public interface IDbConnectionFactory
{
    MotorBaseDatos Motor { get; }
    DbConnection CreateConnection();
}
