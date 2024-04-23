using Application.DTOs.Response;
using Application.UseCases.Usuarios.GetById;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{

    public class GetUserByIdUseCaseTest : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public GetUserByIdUseCaseTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void SHouldReturnTheUsuarioWithTheSameId()
        {

            var usuario1 = CreateValidUsuario();
            var listResult = new List<Usuario>() { usuario1 };

            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(listResult.First(x => x.Id == usuario1.Id));

            var uc = new GetUserByIdUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1.Id);

            result.Data.Should().NotBeNull();
            result.Data!.Id.Should().Be(usuario1.Id);
            result.Data!.Nome.Should().Be(usuario1.Nome);
            result.Data!.Email.Should().Be(usuario1.Email);
            result.Data!.CPF.Should().Be(usuario1.CPF);
            result.Data!.Saldo.Should().Be(usuario1.Saldo);
        }


        [Fact]
        public async void GivenAIdAndUserWasNotFoundShouldReturnErrorStatus()

        {
            var usuario1 = CreateValidUsuario();
            var listResult = new List<Usuario>() { };

            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()));
            var uc = new GetUserByIdUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1.Id);
            result.Data.Should().BeNull();
            result.Status.Should().Be(StatusCodeObjectResponse.NotFound);
            result.Message[0].Should().Be("Usuario nao encontrado");
        }
    }
}
