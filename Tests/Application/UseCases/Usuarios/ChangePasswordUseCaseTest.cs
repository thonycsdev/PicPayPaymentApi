using Application.DTOs.Response;
using Application.UseCases.Usuarios.ChangePassword;
using AutoFixture;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Infra.RepositoriesInterfaces;
using Moq;

namespace Tests.Application.UseCases.Usuarios
{

    public class ChangePasswordUseCaseTest
    {
        public Mock<IUsuarioRepository> _repositoryMock;
        public Fixture _fixture;
        public Faker _faker;
        public ChangePasswordUseCaseTest()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            _repositoryMock = new Mock<IUsuarioRepository>();
        }

        [Fact]
        public async void ShouldUpdateTheUserPassword()
        {
            var usuario1 = _fixture.Create<Usuario>();

            var newPassword = _faker.Internet.Password();

            _repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(usuario1);
            var uc = new ChangePasswordUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1.Id, newPassword);

            usuario1.Senha.Should().Be(newPassword);
            _repositoryMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
            _repositoryMock.Verify(x => x.Commit(), Times.Once);


        }
        [Fact]
        public async void ShouldReturnErrorWhenUserInsertInvalidPassword()
        {
            var usuario1 = _fixture.Create<Usuario>();

            var newPassword = string.Empty;

            var uc = new ChangePasswordUseCase(_repositoryMock.Object);

            var result = await uc.Handle(usuario1.Id, newPassword);
            result.Status.Should().Be(StatusCodeObjectResponse.Error);
            _repositoryMock.Verify(x => x.Commit(), Times.Never);

        }
    }
}
