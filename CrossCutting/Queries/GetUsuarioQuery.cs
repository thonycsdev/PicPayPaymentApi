using Application.DTOs.Response;
using MediatR;

namespace CrossCutting.Queries
{
    public class GetUsuarioQuery : IRequest<ObjectResponse<UsuarioResponse>>
    {
        public Guid Id { get; set; }
    }
}

