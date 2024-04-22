using AutoFixture;
using Bogus;
using Domain.Entities;
using Domain.EntitiesExceptions;
using FluentAssertions;

namespace Tests.Application.Entities
{

    public class BaseEntityValidationTests
    {
        private readonly Fixture _fixture;
        private readonly Faker _faker;

        public BaseEntityValidationTests()
        {
            _fixture = new Fixture();
            _faker = new Faker();
        }
        [Fact]
        public void GivenCorrectValuesOfDataShouldReturnAEmptyListAsErrorMsg()
        {
            var usuario = _fixture.Create<Usuario>();
            usuario.Email = _faker.Person.Email;

            var validator = new BaseEntityValidation();

            var result = validator.Validate(usuario);
            result.Count.Should().Be(0);
        }
        [Theory]
        [InlineData("a@")]
        [InlineData("not_a_email.com")]
        public void GivenAInvalidEmailShouldReturnTheErrorMsgInTheList(string email)
        {
            var validator = new BaseEntityValidation();

            validator.ValidateEmail(email);
            validator.ErrosList.Count.Should().Be(1);
        }
        [Theory]
        [InlineData("a@")]
        [InlineData("morethen255Charaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void GivenAInvalidPasswordShouldAddAErrorMsgTotheList(string password)
        {
            var validator = new BaseEntityValidation();

            validator.ValidadePassword(password);
            validator.ErrosList.Count.Should().Be(1);
        }
        [Fact]
        public void GivenANegativeSaldoShouldAddAErrorToTheList()
        {
            var validator = new BaseEntityValidation();
            var saldo = -3f;

            validator.ValidateSaldoInicial(saldo);
            validator.ErrosList.Count.Should().Be(1);
        }
    }
}
