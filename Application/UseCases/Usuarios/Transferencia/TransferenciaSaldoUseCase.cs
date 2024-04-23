using Application.DTOs.Response;
using AutoMapper;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.Transferencia
{
    public class TransferenciaUseCase : ITransferenciaSaldoUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        public TransferenciaUseCase(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<(ObjectResponse<UsuarioResponse>, ObjectResponse<UsuarioResponse>)> Handle(Guid payer_id, Guid receiver_id, float amount)
        {
            var rsp_payer = new ObjectResponse<UsuarioResponse>();
            var rsp_receiver = new ObjectResponse<UsuarioResponse>();
            var payer = await _repository.GetById(payer_id);
            var receiver = await _repository.GetById(receiver_id);

            if (payer.Saldo < amount)
            {
                return (rsp_payer.ReturnError(null), rsp_receiver.ReturnSucess(null));
            }

            payer.Saldo -= amount;
            receiver.Saldo += amount;

            _repository.Update(payer);
            _repository.Update(receiver);

            await _repository.Commit();

            var response_payer = _mapper.Map<UsuarioResponse>(payer);
            var response_receiver = _mapper.Map<UsuarioResponse>(receiver);

            return (rsp_payer.ReturnSucess(response_payer), rsp_receiver.ReturnSucess(response_receiver));
        }
    }
}

