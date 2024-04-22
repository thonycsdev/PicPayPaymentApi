
using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.GetById
{
    public interface IGetUsuarioByIdUseCase
    {
        Task<ObjectResponse<UsuarioResponse>> Handle(Guid id);
    }
}
