using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.EntitiesExceptions;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.Create
{
    public class CreateUsuarioUseCase : ICreateUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        public CreateUsuarioUseCase(IUsuarioRepository repository)
        {
            _repository = repository;

        }

        public ObjectResponse<UsuarioResponse> Handle(UsuarioRequest input)
        {

            var entity = UsuarioRequest.ToEntity(input);
            var validator = new BaseEntityValidation();
            var validationResults = validator.Validate(entity);
            var rsp = new ObjectResponse<UsuarioResponse>();
            if (validationResults.Count > 0)
            {
                rsp.AddErrors(validationResults);
                return rsp.ReturnError(null);

            }
            _repository.Create(entity);
            _repository.Commit();

            return rsp.ReturnSucess(null);

        }
    }
}
