using Application.DTOs.Response;
using Domain.Entities;
using MediatR;

namespace Application.DTOs.Request
{
    public class LojistaRequest : IRequest<ObjectResponse<LojistaResponse>>
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public float Saldo { get; set; } = 0f;
        public string CNPJ { get; set; } = string.Empty;

        public static Lojista ToEntity(LojistaRequest req)
        {
            var lojista = new Lojista()
            {
                Nome = req.Nome,
                CNPJ = req.CNPJ,
                Saldo = req.Saldo,
                Senha = req.Senha,
                Email = req.Email
            };

            return lojista;
        }
    }
}
