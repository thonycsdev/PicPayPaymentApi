namespace Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public float Saldo { get; set; } = 0f;

    }
}
