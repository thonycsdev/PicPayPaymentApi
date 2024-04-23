using Application.AutoMapper;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Bogus.Extensions.Brazil;
using Bogus;
namespace Tests.Commom
{

    public class CommomTestFixture
    {
        public Fixture _fixture;
        public Faker _faker;
        public IMapper _mapper;

        public CommomTestFixture()
        {
            _fixture = new Fixture();
            _faker = new Faker();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfigProfile());
            });

            _mapper = mockMapper.CreateMapper();
        }



        public Usuario CreateValidUsuario()
        {
            var usuario = new Usuario();
            usuario.Id = Guid.NewGuid();
            usuario.Nome = _faker.Person.FullName;
            usuario.CPF = _faker.Person.Cpf();
            usuario.Email = _faker.Person.Email;
            usuario.Senha = _faker.Internet.Password();
            return usuario;
        }
    }

}
