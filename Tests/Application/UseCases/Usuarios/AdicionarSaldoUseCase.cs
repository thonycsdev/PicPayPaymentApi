using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.AdicionarSaldo;
using Application.UseCases.Usuarios.Delete;
using AutoFixture;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;

namespace Tests.Application.UseCases.Usuarios
{

    public class AdicionarSaldoUseCaseTest
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public Fixture _fixture;
        public Faker _faker;
        public AdicionarSaldoUseCaseTest()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldUpdateTheOldUsuarioDataWithTheNewOne()
        {
            var usuario = _fixture.Create<Usuario>();
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
            var usuario2 = _fixture.Create<Usuario>();
            _repositoryMock.Setup(x => x.GetById(usuario2.Id));

            var uc = new UpdateUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1, usuario2.Id);

            result.Status.Should().Be(StatusCodeObjectResponse.Error);

        }
    }
}
