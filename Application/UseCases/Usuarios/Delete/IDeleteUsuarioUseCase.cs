using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.Delete
{

    public interface IDeleteUsuarioUseCase
    {
        Task<ObjectResponse<UsuarioResponse>> Handle(Guid id);
    }

}
