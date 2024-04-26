using Application.DTOs.Response;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Usuarios.Delete
{
    public class DeleteUsuarioById : IRequest<ObjectResponse<UsuarioResponse>>
    {

        public Guid Id { get; set; }
        public DeleteUsuarioById(Guid id)
        {
            Id = id;
        }
    }
}
