using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.GetById
{
    public interface IGetUsuarioByIdUseCase
        : IRequestHandler<GetUsuarioByIdQuery, ObjectResponse<UsuarioResponse>> { }
}
