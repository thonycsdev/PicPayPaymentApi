using Application.DTOs.Response;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.Delete
{
    public class DeleteUsuarioUseCase : IDeleteUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;

        public DeleteUsuarioUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObjectResponse<UsuarioResponse>> Handle(
            DeleteUsuarioById request,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.GetById(request.Id);
            var rsp = new ObjectResponse<UsuarioResponse>();
            if (entity is null)
                return rsp.ReturnNotFound(null);

            _repository.Delete(entity);
            await _repository.Commit();
            return rsp.ReturnSucess(null);
        }
    }
}
