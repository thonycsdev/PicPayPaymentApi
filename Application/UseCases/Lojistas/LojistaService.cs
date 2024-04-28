using Application.DTOs.Response;
using AutoMapper;
using Infra.RepositoriesInterfaces;

namespace Application.UseCase.Lojistas
{
    public class LojistasService : ILojistaService
    {
        private readonly ILojistaRepository _repository;
        private readonly IMapper _mapper;

        public LojistasService(ILojistaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ObjectResponse<List<LojistaResponse>>> GetAllLojistas()
        {
            var entities = await _repository.GetAll();
            var rsp = new ObjectResponse<List<LojistaResponse>>();
            var response = _mapper.Map<List<LojistaResponse>>(entities);
            return rsp.ReturnSucess(response);
        }

        public async Task<ObjectResponse<LojistaResponse>> GetLojistaById(Guid id)
        {
            var entity = await _repository.GetById(id);
            var rsp = new ObjectResponse<LojistaResponse>();
            if (entity is null)
            {
                rsp.AddError("Id not found");
                return rsp.ReturnError(null);
            }

            var response = _mapper.Map<LojistaResponse>(entity);
            return rsp.ReturnSucess(response);
        }
    }
}
