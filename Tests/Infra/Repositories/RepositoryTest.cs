using Domain.Entities;
using FluentAssertions;
using Infra;
using Infra.Repositories;
using Tests.Commom;

namespace Tests.Infra.Repositories
{
    public class RepositoryTests : CommomTestFixture
    {
        private readonly DatabaseContext _context;

        public RepositoryTests()
        {
            _context = CreateInMemoryDatabaseContext();
        }

        [Fact]
        public async void ShouldAddAUsuarioInTheDatabaseList()
        {
            var usuario = CreateValidUsuario();
            var repo = new Repository<Usuario>(_context);
            await repo.Create(usuario);
            await repo.Commit();

            var result = _context.Set<Usuario>().ToList();
            result.Count.Should().Be(1);
        }

        [Fact]
        public async void ShouldAddAndThenDeleteTheInsertedUser()
        {
            var usuario = CreateValidUsuario();
            var repo = new Repository<Usuario>(_context);
            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();
            repo.Delete(usuario);
            await repo.Commit();
            var result = _context.Set<Usuario>().ToList();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async void ShouldFindTheAddedUsuarioByHisId()
        {
            var usuario = CreateValidUsuario();
            var repo = new Repository<Usuario>(_context);
            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();

            var result = await repo.GetById(usuario.Id);

            result.Should().NotBeNull();

            result.Id.Should().Be(usuario.Id);
        }

        [Fact]
        public async void ShouldReturnTheListWithTheCorrectCountOfUsuariosInDatabase()
        {
            var usuarios = new List<Usuario>();
            for (int x = 0; x < 5; x++)
            {
                var usuario = CreateValidUsuario();
                usuarios.Add(usuario);
            }
            var repo = new Repository<Usuario>(_context);
            await _context.Set<Usuario>().AddRangeAsync(usuarios);
            await _context.SaveChangesAsync();
            var results = await repo.GetAll();
            results.Should().NotBeEmpty();
            results.Count.Should().Be(5);
        }

        [Fact]
        public async void ShouldCallUpdateFunctionAndReturnTheUpdatedEntity()
        {
            var usuario = CreateValidUsuario();
            var repo = new Repository<Usuario>(_context);
            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();

            var result = repo.Update(usuario);
            result.Id.Should().Be(usuario.Id);
        }

        //update
    }
}
