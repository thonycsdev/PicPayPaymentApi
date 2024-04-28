using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.Transferencia
{
    public interface ITransferenciaSaldoUseCase
    {
        Task<(ObjectResponse<UsuarioResponse>, ObjectResponse<UsuarioResponse>)> Handle(
            Guid payer_id,
            Guid receiver_id,
            float amount
        );
    }
}
