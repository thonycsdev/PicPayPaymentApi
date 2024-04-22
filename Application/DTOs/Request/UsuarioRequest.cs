using Domain.Entities;

namespace Application.DTOs.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public float Saldo { get; set; } = 0f;
        public string CPF { get; set; } = string.Empty;


        public static Usuario ToEntity(UsuarioRequest req)
        {
            var usuario = new Usuario
            {
                Nome = req.Nome,
                CPF = req.CPF,
                Saldo = req.Saldo,
                Senha = req.Senha,
                Email = req.Email
            };
            return usuario;
        }

    }

}
