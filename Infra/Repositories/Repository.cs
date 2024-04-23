using Domain.Entities;
using Infra.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _entity;
        private readonly DatabaseContext _context;
        public Repository(DatabaseContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Create(T entity)
        {
            await _entity.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
            var result = await _entity.ToListAsync();
            return result;
        }

        public async Task<T> GetById(Guid id)
        {
            var result = await _entity.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
