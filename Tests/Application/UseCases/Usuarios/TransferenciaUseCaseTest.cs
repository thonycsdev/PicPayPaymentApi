using Application.AutoMapper;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Usuarios.AdicionarSaldo;
using Application.UseCases.Usuarios.Delete;
using Application.UseCases.Usuarios.Transferencia;
using AutoFixture;
using AutoMapper;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;

namespace Tests.Application.UseCases.Usuarios
{

    public class TransferenciaUseCaseTest
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public Fixture _fixture;
        public Faker _faker;
        public IMapper _mapper;
        public TransferenciaUseCaseTest()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            _repositoryMock = new Mock<IUsuarioRepository>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfigProfile()); //your automapperprofile 
            });

            _mapper = mockMapper.CreateMapper();
        }


        [Fact]
        public async void ShouldReturnErrorWhenUsuario1DoesntHaveEnoughSaldoToTransfer()
        {

            var usuario1 = _fixture.Create<Usuario>();
            var usuario2 = _fixture.Create<Usuario>();

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

            var usuario1 = _fixture.Create<Usuario>();
            var usuario2 = _fixture.Create<Usuario>();
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

            var usuario1 = _fixture.Create<Usuario>();
            var usuario2 = _fixture.Create<Usuario>();
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
