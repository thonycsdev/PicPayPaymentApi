using Application.DTOs.Response;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.GetAll
{

    public class GetAllUsuariosUseCase : IGetAllUsuariosUseCase
    {
        private readonly IUsuarioRepository _repository;
        public GetAllUsuariosUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<ObjectResponse<List<UsuarioResponse>>> Handle()
        {
            var results = await _repository.GetAll();

            var rsp = new ObjectResponse<List<UsuarioResponse>>();
            var response = results.Select(x =>
            {
                var output = new UsuarioResponse()
                {
                    Nome = x.Nome,
                    Email = x.Email,
                    CPF = x.CPF,
                    Saldo = x.Saldo,
                    Id = x.Id
                };
                return output;
            }).ToList();
            rsp.Status = StatusCodeObjectResponse.Sucess;
            rsp.Data = response;
            return rsp;
        }
    }
}
