using Application.DTOs.Request;
using Application.DTOs.Response;
using Domain.EntitiesExceptions;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Lojistas.AdicionarLojista
{
    public class AdicionarLojistaUseCase : IAdicionarLojistaUseCase
    {
        private readonly ILojistaRepository _repository;
        public AdicionarLojistaUseCase(ILojistaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ObjectResponse<LojistaResponse>> Handle(LojistaRequest request, CancellationToken cancellationToken)
        {
            var entity = LojistaRequest.ToEntity(request);
            var validator = new BaseEntityValidation();
            var rsp = new ObjectResponse<LojistaResponse>();
            var validatorResults = validator.Validate(entity);
            if (validatorResults.Count > 0)
            {
                rsp.AddErrors(validatorResults);
                rsp.ReturnError(null);
            }
            await _repository.Create(entity);
            await _repository.Commit();

            return rsp.ReturnSucess(null);

        }
    }

}
