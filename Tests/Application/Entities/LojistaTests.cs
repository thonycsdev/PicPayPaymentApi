using Domain.Entities;
using FluentAssertions;

namespace Tests.Application.Entities
{
    public class LojistaTests
    {
        [Fact]
        public void UsuarioShoulHaveCPFProperty()
        {
            var usuario = new Lojista();
            usuario.GetType().GetProperties().Should().Contain(x => x.Name == "CNPJ");
        }
    }
}
