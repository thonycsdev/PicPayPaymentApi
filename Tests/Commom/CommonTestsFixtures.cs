using Application.AutoMapper;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Bogus.Extensions.Brazil;
using Bogus;
using Infra;
using Microsoft.EntityFrameworkCore;
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


        public Lojista CreateValidLojista()
        {
            var lojista = new Lojista();
            lojista.Id = Guid.NewGuid();
            lojista.Nome = _faker.Person.FullName;
            lojista.CNPJ = _faker.Company.Cnpj();
            lojista.Email = _faker.Person.Email;
            lojista.Senha = _faker.Internet.Password();
            return lojista;
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


        public DatabaseContext CreateInMemoryDatabaseContext()
        {
            var opt = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: DateTime.Now.Ticks.ToString()).Options;
            var context = new DatabaseContext(opt);
            return context;
        }
    }

}
