using Application.DTOs.Response;
using Application.UseCases.Usuarios.Delete;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{
    public class DeleteUsuarioUseCaseTest : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;

        public DeleteUsuarioUseCaseTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldReturn200WhenTheUsuarioGetsDeleted()
        {
            var usuario1 = CreateValidUsuario();
            var usuario2 = CreateValidUsuario();

            var listaUsuario = new List<Usuario>() { usuario1, usuario2 };
            var input = new DeleteUsuarioById(usuario1.Id);

            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(usuario1);
            _repositoryMock
                .Setup(x => x.Delete(usuario1))
                .Callback<Usuario>(x => listaUsuario.Remove(x));

            var uc = new DeleteUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(input, CancellationToken.None);

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            listaUsuario.Count.Should().Be(2 - 1);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async void ShouldReturnNotFoundWhenTheUserIdDoesntMatch()
        {
            var usuario1 = CreateValidUsuario();

            var input = new DeleteUsuarioById(usuario1.Id);
            var listaUsuario = new List<Usuario>() { usuario1 };
            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()));
            var uc = new DeleteUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(input, CancellationToken.None);

            result.Status.Should().Be(StatusCodeObjectResponse.NotFound);
        }
    }
}
