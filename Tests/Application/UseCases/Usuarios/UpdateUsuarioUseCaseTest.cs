using Application.DTOs.Response;
using Application.UseCases.Usuarios.Delete;
using Application.UseCases.Usuarios.Update;
using AutoFixture;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{
    public class UpdateUsuarioUseCaseTest : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;

        public UpdateUsuarioUseCaseTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldUpdateTheOldUsuarioDataWithTheNewOne()
        {
            var usuario1 = _fixture.Create<UpdateUsuarioRequest>();
            var usuario2 = CreateValidUsuario();
            usuario2.Id = usuario1.usuario_toEdit_Id;
            _repositoryMock.Setup(x => x.GetById(usuario2.Id)).ReturnsAsync(usuario2);

            var uc = new UpdateUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1, CancellationToken.None);

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);
            result.Data!.Id.Should().Be(usuario2.Id);
            result.Data.Nome.Should().Be(usuario1.request.Nome);
            result.Data.Email.Should().Be(usuario1.request.Email);
            result.Data.CPF.Should().Be(usuario1.request.CPF);
            result.Data.Saldo.Should().Be(usuario1.request.Saldo);
        }
    }
}
