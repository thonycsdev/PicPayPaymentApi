using Application.DTOs.Response;
using Domain.EntitiesExceptions;
using Infra.RepositoriesInterfaces;

namespace Application.UseCases.Usuarios.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IUsuarioRepository _repository;
        public ChangePasswordUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<ObjectResponse<UsuarioResponse>> Handle(Guid id, string newPassword)
        {
            var validator = new BaseEntityValidation();
            var rsp = new ObjectResponse<UsuarioResponse>();
            validator.ValidadePassword(newPassword);
            if (validator.ErrosList.Count > 0)
                return rsp.ReturnError(null);

            var usuario = await _repository.GetById(id);
            usuario.Senha = newPassword;
            await _repository.Update(usuario);
            await _repository.Commit();
            var response = new UsuarioResponse()
            {
                Email = usuario.Email,
                Id = usuario.Id,
                CPF = usuario.CPF,
                Saldo = usuario.Saldo,
                Nome = usuario.Nome
            };
            return rsp.ReturnSucess(response);
            throw new NotImplementedException();

        }
    }
}
