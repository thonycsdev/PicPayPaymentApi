using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.Delete;
using AutoFixture;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;

namespace Tests.Application.UseCases.Usuarios
{

    public class UpdateUsuarioUseCaseTest
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public Fixture _fixture;
        public Faker _faker;
        public UpdateUsuarioUseCaseTest()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldUpdateTheOldUsuarioDataWithTheNewOne()
        {
            var usuario1 = _fixture.Create<UsuarioRequest>();
            var usuario2 = _fixture.Create<Usuario>();
            _repositoryMock.Setup(x => x.GetById(usuario2.Id)).ReturnsAsync(usuario2);

            var uc = new UpdateUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1, usuario2.Id);

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);
            result.Data!.Id.Should().Be(usuario2.Id);
            result.Data.Nome.Should().Be(usuario1.Nome);
            result.Data.Email.Should().Be(usuario1.Email);
            result.Data.CPF.Should().Be(usuario1.CPF);
            result.Data.Saldo.Should().Be(usuario1.Saldo);

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
