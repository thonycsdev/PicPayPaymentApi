using Application.DTOs.Response;
using Application.UseCases.Usuarios.GetAll;
using AutoFixture;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;

namespace Tests.Application.UseCases.Usuarios
{

    public class GetAllUseCaseTest
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public Fixture _fixture;
        public Faker _faker;
        public GetAllUseCaseTest()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldReturnTheListOfUsuarioResponse()
        {

            var usuarios = new List<Usuario>();
            for (int x = 0; x < 5; x++)
                usuarios.Add(_fixture.Create<Usuario>());

            _repositoryMock.Setup(x => x.GetAll()).ReturnsAsync(usuarios);

            var uc = new GetAllUsuariosUseCase(_repositoryMock.Object);

            var results = await uc.Handle();

            results.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            results.Data.Should().HaveCount(5);
        }


    }
}
