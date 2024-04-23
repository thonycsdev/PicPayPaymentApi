using Application.DTOs.Response;
using Application.UseCases.Usuarios.Transferencia;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Usuarios
{

    public class TransferenciaUseCaseTest : CommomTestFixture
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public TransferenciaUseCaseTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
        }


        [Fact]
        public async void ShouldReturnErrorWhenUsuario1DoesntHaveEnoughSaldoToTransfer()
        {

            var usuario1 = CreateValidUsuario();
            var usuario2 = CreateValidUsuario();

            _repositoryMock.Setup(x => x.GetById(usuario1.Id)).ReturnsAsync(usuario1);
            _repositoryMock.Setup(x => x.GetById(usuario2.Id)).ReturnsAsync(usuario2);
            usuario1.Saldo = 0f;

            var uc = new TransferenciaUseCase(_repositoryMock.Object, _mapper);

            var result = await uc.Handle(usuario1.Id, usuario2.Id, 1200f);
            result.Item1.Status.Should().Be(StatusCodeObjectResponse.Error);
        }

        [Fact]
        public async void PayerSaldoShouldHaveLessTheValueGiven()
        {

            var usuario1 = CreateValidUsuario();
            var usuario2 = CreateValidUsuario();
            var oldSaldo = 1201f;
            var value = 1200f;
            _repositoryMock.Setup(x => x.GetById(usuario1.Id)).ReturnsAsync(usuario1);
            _repositoryMock.Setup(x => x.GetById(usuario2.Id)).ReturnsAsync(usuario2);
            usuario1.Saldo = oldSaldo;


            var uc = new TransferenciaUseCase(_repositoryMock.Object, _mapper);

            var result = await uc.Handle(usuario1.Id, usuario2.Id, value);
            result.Item1.Data!.Saldo.Should().Be(oldSaldo - value);
        }
        [Fact]
        public async void ReceiverSaldoShouldIncreseByTheValueGiven()
        {

            var usuario1 = CreateValidUsuario();
            var usuario2 = CreateValidUsuario();
            _repositoryMock.Setup(x => x.GetById(usuario1.Id)).ReturnsAsync(usuario1);
            _repositoryMock.Setup(x => x.GetById(usuario2.Id)).ReturnsAsync(usuario2);

            usuario1.Saldo = 3000f;
            usuario2.Saldo = 1500f;


            var uc = new TransferenciaUseCase(_repositoryMock.Object, _mapper);

            var result = await uc.Handle(usuario1.Id, usuario2.Id, 1500f);
            result.Item2.Data!.Saldo.Should().Be(1500f + 1500f);
        }

    }
}
