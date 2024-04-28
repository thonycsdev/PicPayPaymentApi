using Domain.Entities;

namespace Domain.EntitiesExceptions
{
    public class BaseEntityValidation
    {
        public List<string> ErrosList = new List<string>();

        public List<string> Validate(BaseEntity entity)
        {
            ValidateNome(entity.Nome)
                .ValidateEmail(entity.Email)
                .ValidadePassword(entity.Senha)
                .ValidateSaldoInicial(entity.Saldo);
            return this.ErrosList;
        }

        public BaseEntityValidation ValidateNome(string nome)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrWhiteSpace(nome))
                ErrosList.Add("Nome invalido");
            return this;
        }

        public BaseEntityValidation ValidateEmail(string email)
        {
            const string ERRO_MSG = "Email invalido";

            if (!email.Any(x => x == '@'))
                ErrosList.Add(ERRO_MSG);

            if (email.Length >= 255)
                ErrosList.Add(ERRO_MSG);
            if (email.Length < 5)
                ErrosList.Add(ERRO_MSG);

            return this;
        }

        public BaseEntityValidation ValidadePassword(string password)
        {
            const string ERRO_MSG = "Senha invalida";

            if (password.Length >= 255 || password.Length <= 5 || string.IsNullOrEmpty(password))
                ErrosList.Add(ERRO_MSG);

            return this;
        }

        public BaseEntityValidation ValidateSaldoInicial(float saldo)
        {
            const string ERRO_MSG = "Saldo invalido";

            if (saldo < 0)
                ErrosList.Add(ERRO_MSG);

            return this;
        }
    }
}
