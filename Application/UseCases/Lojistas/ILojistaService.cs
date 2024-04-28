using Application.DTOs.Response;

namespace Application.UseCase.Lojistas
{
    public interface ILojistaService
    {
        Task<ObjectResponse<LojistaResponse>> GetLojistaById(Guid id);
        Task<ObjectResponse<List<LojistaResponse>>> GetAllLojistas();
    }
}
