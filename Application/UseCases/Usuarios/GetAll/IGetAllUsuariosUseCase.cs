using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.GetAll
{
    public interface IGetAllUsuariosUseCase
    {
        Task<ObjectResponse<List<UsuarioResponse>>> Handle();
    }
}
