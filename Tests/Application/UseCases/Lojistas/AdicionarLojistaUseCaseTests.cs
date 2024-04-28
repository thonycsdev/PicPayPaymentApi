using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.UseCases.Lojistas.AdicionarLojista;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Lojistas.AdicionarLojista
{
    public class AdicionarLojistaUseCaseTest : CommomTestFixture
    {
        private readonly Mock<ILojistaRepository> _repositoryMock;

        public AdicionarLojistaUseCaseTest()
        {
            _repositoryMock = new Mock<ILojistaRepository>();
        }

        [Fact]
        public async void GivenAValidLojistaDataShouldAddTheLojistaToTheList()
        {
            var inputRequest = _fixture.Create<LojistaRequest>();

            var list = new List<Lojista>();
            _repositoryMock
                .Setup(x => x.Create(It.IsAny<Lojista>()))
                .Callback<Lojista>(x => list.Add(x));

            var uc = new AdicionarLojistaUseCase(_repositoryMock.Object);

            var result = await uc.Handle(inputRequest, CancellationToken.None);

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            list.Count.Should().Be(1);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);
        }

        //criar teste para retornar com o erro caso a validacao aponte alguma coisa
    }
}
