using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.Create;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{

    public class CreateUsuarioUseCaseTests : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public CreateUsuarioUseCaseTests()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public void ShouldCreateANewUsuarioOnTheDatabase()
        {

            var usuarioInput = _fixture.Create<UsuarioRequest>();
            usuarioInput.Email = _faker.Person.Email;
            var uc = new CreateUsuarioUseCase(_repositoryMock.Object);

            var listaUsuarios = new List<Usuario>();
            _repositoryMock.Setup(x => x.Create(It.IsAny<Usuario>())).Callback<Usuario>(x =>
            {
                listaUsuarios.Add(x);
            });
            var result = uc.Handle(usuarioInput);

            listaUsuarios.Count.Should().Be(1);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);
            result.Message.Should().BeEmpty();
            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
        }

        [Fact]

        public void NotGivenANameInUsuarioRequestShouldReturnObjectErrorWithAMessage()
        {
            var usuarioInput = _fixture.Build<UsuarioRequest>().Without(x => x.Nome).Create();
            var uc = new CreateUsuarioUseCase(_repositoryMock.Object);
            var result = uc.Handle(usuarioInput);
            result.Status.Should().Be(StatusCodeObjectResponse.Error);
            result.Message[0].Should().Be("Nome invalido");
        }
    }
}
