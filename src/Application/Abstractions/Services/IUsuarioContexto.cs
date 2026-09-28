namespace BancoDistribuido.Application.Abstractions.Services;

/// <summary>Usuario que se registra en la auditoría (p_user_id_insert).</summary>
public interface IUsuarioContexto
{
    string UserId { get; }
}
