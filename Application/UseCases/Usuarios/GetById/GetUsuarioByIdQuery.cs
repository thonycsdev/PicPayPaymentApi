using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.GetById
{
    public class GetUsuarioByIdQuery : IRequest<ObjectResponse<UsuarioResponse>>
    {
        public Guid Id { get; set; }

        public GetUsuarioByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
