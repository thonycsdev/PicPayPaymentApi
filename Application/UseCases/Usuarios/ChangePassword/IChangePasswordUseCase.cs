using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task<ObjectResponse<UsuarioResponse>> Handle(Guid id, string newPassword);
    }
}
