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

    public class DeleteUsuarioUseCaseTest
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public Fixture _fixture;
        public Faker _faker;
        public DeleteUsuarioUseCaseTest()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldReturn200WhenTheUsuarioGetsDeleted()
        {
            var usuario1 = _fixture.Create<Usuario>();
            var usuario2 = _fixture.Create<Usuario>();

            var listaUsuario = new List<Usuario>() { usuario1, usuario2 };
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(usuario1);
            _repositoryMock.Setup(x => x.Delete(usuario1)).Callback<Usuario>(x => listaUsuario.Remove(x));
            var uc = new DeleteUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1.Id);

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            listaUsuario.Count.Should().Be(2 - 1);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);

        }
        [Fact]
        public async void ShouldReturnNotFoundWhenTheUserIdDoesntMatch()
        {
            var usuario1 = _fixture.Create<Usuario>();

            var listaUsuario = new List<Usuario>() { usuario1 };
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()));
            var uc = new DeleteUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1.Id);

            result.Status.Should().Be(StatusCodeObjectResponse.NotFound);

        }


    }
}
