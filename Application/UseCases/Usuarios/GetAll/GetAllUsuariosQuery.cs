using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.GetAll
{
    public class GetAllUsuariosQuery : IRequest<ObjectResponse<IEnumerable<UsuarioResponse>>> { }
}
