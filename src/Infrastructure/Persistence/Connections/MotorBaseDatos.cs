namespace BancoDistribuido.Infrastructure.Persistence.Connections;

/// <summary>Llave de cada conexión distribuida (se usa como keyed service).</summary>
public enum MotorBaseDatos
{
    /// <summary>MovimientosBD: cuentas, saldos, transacciones y los 3 SPs.</summary>
    PostgreSql,

    /// <summary>ClientesBD: cliente y su estado.</summary>
    SqlServer
}
