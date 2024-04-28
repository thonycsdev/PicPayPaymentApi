using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.GetAll
{
    public interface IGetAllUsuariosUseCase
        : IRequestHandler<GetAllUsuariosQuery, ObjectResponse<IEnumerable<UsuarioResponse>>> { }
}
