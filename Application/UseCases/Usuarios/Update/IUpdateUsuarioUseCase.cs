using Application.DTOs.Request;
using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.Update
{
    public interface IUpdateUsuarioUseCase
    {
        Task<ObjectResponse<UsuarioResponse>> Handle(UsuarioRequest req, Guid id);
    }
}
