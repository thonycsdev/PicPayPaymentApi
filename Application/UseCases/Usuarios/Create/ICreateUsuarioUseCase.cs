using Application.DTOs.Request;
using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.Create
{
    public interface ICreateUsuarioUseCase
    {
        ObjectResponse<UsuarioResponse> Handle(UsuarioRequest req);
    }
}
