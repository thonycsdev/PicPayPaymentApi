using Application.DTOs.Response;
using Application.UseCases.Usuarios.AdicionarSaldo;
using Application.UseCases.Usuarios.Delete;
using Application.UseCases.Usuarios.Update;
using AutoFixture;
using Domain.Entities;
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
            var request = _fixture.Create<UpdateUsuarioRequest>();

            var usuario = _fixture.Create<Usuario>();

            usuario.Id = request.usuario_toEdit_Id;

            _repositoryMock.Setup(x => x.GetById(usuario.Id));
            var uc = new UpdateUsuarioUseCase(_repositoryMock.Object);

            var result = await uc.Handle(request, CancellationToken.None);

            result.Status.Should().Be(StatusCodeObjectResponse.Error);
        }
    }
}
