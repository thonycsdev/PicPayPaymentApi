using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.AdicionarSaldo;
using Application.UseCases.Usuarios.Delete;
using AutoFixture;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{

    public class AdicionarSaldoUseCaseTest : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public AdicionarSaldoUseCaseTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldUpdateTheOldUsuarioDataWithTheNewOne()
        {
            var usuario = CreateValidUsuario();
            var oldSaldo = usuario.Saldo;
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(usuario);

            var input = new
            {
                id = usuario.Id,
                valor = 1200
            };

            var uc = new AdicionarSaldoUseCase(_repositoryMock.Object);

            var result = await uc.Handle(input.id, input.valor);

            var sum = oldSaldo + input.valor;
            usuario.Saldo.Should().Be(sum);
            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);


        }
        [Fact]
        public async void ShouldReturnNotFoundWhenUsuarioWithIdWasNotMatched()
        {
            var usuario1 = _fixture.Create<UsuarioRequest>();
            var usuario2 = CreateValidUsuario();
            _repositoryMock.Setup(x => x.GetById(usuario2.Id));

            var uc = new UpdateUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1, usuario2.Id);

            result.Status.Should().Be(StatusCodeObjectResponse.Error);

        }
    }
}
