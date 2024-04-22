using Domain.Entities;
using FluentAssertions;

namespace Tests.Application.Entities
{
    public class UsuarioTests
    {

        [Fact]
        public void UsuarioShoulHaveCPFProperty()
        {
            var usuario = new Usuario();
            usuario.GetType().GetProperties().Should().Contain(x => x.Name == "CPF");
        }
    }
}
