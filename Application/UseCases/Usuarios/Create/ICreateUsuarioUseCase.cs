using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.Create
{
    public interface ICreateUsuarioUseCase : IRequestHandler<UsuarioRequest, ObjectResponse<UsuarioResponse>>
    {
    }
}
