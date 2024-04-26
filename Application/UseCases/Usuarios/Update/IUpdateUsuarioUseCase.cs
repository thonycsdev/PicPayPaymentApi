using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.Update
{
    public interface IUpdateUsuarioUseCase : IRequestHandler<UpdateUsuarioRequest, ObjectResponse<UsuarioResponse>>
    {
    }
}
