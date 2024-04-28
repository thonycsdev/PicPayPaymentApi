using Application.DTOs.Response;
using Application.UseCase.Lojistas;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;
using Tests.Commom;

namespace Tests.Application.UseCases.Lojistas
{
    public class LojistaServiceTest : CommomTestFixture
    {
        private readonly Mock<ILojistaRepository> _repositoryMoq;

        public LojistaServiceTest()
        {
            _repositoryMoq = new Mock<ILojistaRepository>();
        }

        [Fact]
        public async void GivenALojistaIdShouldReturnTheCorrectLojista()
        {
            var lojista1 = CreateValidLojista();
            var lojistas = new List<Lojista>() { CreateValidLojista() };
            lojistas.Add(lojista1);

            _repositoryMoq
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(lojistas.First(x => x.Id == lojista1.Id));

            var service = new LojistasService(_repositoryMoq.Object, _mapper);

            var result = await service.GetLojistaById(lojista1.Id);

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
        }

        [Fact]
        public async void GivenALojistaIdThatIsntInTheListShouldReturnStatusError()
        {
            {
                var lojista1 = CreateValidLojista();
                var lojistas = new List<Lojista>() { CreateValidLojista() };
                lojistas.Add(lojista1);

                _repositoryMoq.Setup(x => x.GetById(It.IsAny<Guid>()));

                var service = new LojistasService(_repositoryMoq.Object, _mapper);

                var result = await service.GetLojistaById(lojista1.Id);

                result.Status.Should().Be(StatusCodeObjectResponse.Error);
            }
        }

        [Fact]
        public async void ShouldReturnTheListWithAllLojistasInTheList()
        {
            var lojistas = new List<Lojista>();
            for (int x = 0; x < 5; x++)
            {
                lojistas.Add(CreateValidLojista());
            }

            _repositoryMoq.Setup(x => x.GetAll()).ReturnsAsync(lojistas);

            var service = new LojistasService(_repositoryMoq.Object, _mapper);

            var result = await service.GetAllLojistas();

            result.Status.Should().Be(StatusCodeObjectResponse.Sucess);
            result.Data!.Count.Should().Be(5);
        }
    }
}
