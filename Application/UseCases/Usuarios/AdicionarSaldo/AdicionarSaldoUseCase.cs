using Application.DTOs.Response;
using Domain.EntitiesExceptions;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.AdicionarSaldo
{
    public class AdicionarSaldoUseCase : IAdicionarSaldoUseCase
    {
        private readonly IUsuarioRepository _repository;
        public AdicionarSaldoUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<ObjectResponse<string>> Handle(Guid id, float valor)
        {
            var usuario = await _repository.GetById(id);
            var validator = new BaseEntityValidation();
            var rsp = new ObjectResponse<string>();
            validator.ValidateSaldoInicial(valor);
            if (validator.ErrosList.Count > 0)
                return rsp.ReturnError($"Saldo invalido: {valor}");

            usuario.Saldo += valor;
            await _repository.Update(usuario);
            await _repository.Commit();
            return rsp.ReturnSucess($"Novo saldo:{usuario.Saldo}");
        }
    }
}

