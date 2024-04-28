namespace Application.DTOs.Response
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public float Saldo { get; set; } = 0f;
        public string CPF { get; set; } = string.Empty;
    }
}
