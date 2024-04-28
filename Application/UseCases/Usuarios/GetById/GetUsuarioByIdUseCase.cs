using Application.DTOs.Response;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.GetById
{
    public class GetUserByIdUseCase : IGetUsuarioByIdUseCase
    {
        private readonly IUsuarioRepository _repository;

        public GetUserByIdUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObjectResponse<UsuarioResponse>> Handle(
            GetUsuarioByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _repository.GetById(request.Id);
            var rsp = new ObjectResponse<UsuarioResponse>();

            if (result is null)
            {
                rsp.AddError("Usuario nao encontrado");
                return rsp.ReturnNotFound(null);
            }
            var response = new UsuarioResponse();
            response.Id = result!.Id;
            response.Nome = result.Nome;
            response.Email = result.Email;
            response.CPF = result.CPF;
            response.Saldo = result.Saldo;

            return rsp.ReturnSucess(response);
        }
    }
}
