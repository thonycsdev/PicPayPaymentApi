using Application.DTOs.Request;
using Application.DTOs.Response;
using MediatR;

namespace Application.UseCases.Lojistas.AdicionarLojista
{
    public interface IAdicionarLojistaUseCase : IRequestHandler<LojistaRequest, ObjectResponse<LojistaResponse>>
    {

    }

}
