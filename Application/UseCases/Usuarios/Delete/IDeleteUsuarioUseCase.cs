using Application.DTOs.Response;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Usuarios.Delete
{

    public interface IDeleteUsuarioUseCase : IRequestHandler<DeleteUsuarioById, ObjectResponse<UsuarioResponse>>
    {
    }

}
