using Application.DTOs.Response;
using Application.UseCases.Usuarios.GetAll;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{

    public class GetAllUseCaseTest : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public GetAllUseCaseTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldReturnTheListOfUsuarioResponse()
        {

            var usuarios = new List<Usuario>();
            for (int x = 0; x < 5; x++)
                usuarios.Add(CreateValidUsuario());

            _repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(usuarios);

            var uc = new GetAllUsuariosUseCase(_repositoryMock.Object);

            var results = await uc.Handle();

            results.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            results.Data.Should().HaveCount(5);
        }


    }
}
