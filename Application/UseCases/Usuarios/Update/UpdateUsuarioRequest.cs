using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Usuarios.Update
{
    public class UpdateUsuarioRequest : IRequest<ObjectResponse<UsuarioResponse>>
    {
        public Guid usuario_toEdit_Id { get; set; }
        public UsuarioRequest request { get; set; }
    }
}
