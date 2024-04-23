using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.Update;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.Delete
{

    public class UpdateUsuarioUseCase : IUpdateUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        public UpdateUsuarioUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObjectResponse<UsuarioResponse>> Handle(UsuarioRequest req, Guid id)
        {
            var data = UsuarioRequest.ToEntity(req);
            var entity = await _repository.GetById(id);
            var rsp = new ObjectResponse<UsuarioResponse>();
            if (entity is null)
                return rsp.ReturnError(null);
            entity.Nome = data.Nome;
            entity.Saldo = data.Saldo;
            entity.Email = data.Email;
            entity.CPF = data.CPF;
            _repository.Update(entity);
            await _repository.Commit();
            var usuarioResponse = new UsuarioResponse()
            {
                Id = entity.Id,
                Saldo = entity.Saldo,
                CPF = entity.CPF,
                Email = entity.Email,
                Nome = entity.Nome
            };
            return rsp.ReturnSucess(usuarioResponse);

        }
    }
}

