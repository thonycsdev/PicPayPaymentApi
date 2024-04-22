
using Application.DTOs.Response;

namespace Application.UseCases.Usuarios.AdicionarSaldo
{

    public interface IAdicionarSaldoUseCase
    {
        Task<ObjectResponse<string>> Handle(Guid id, float valor);
    }
}
