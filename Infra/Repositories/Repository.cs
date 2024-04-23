using Infra.RepositoriesInterfaces;

namespace Infra.Repositories
{

    public class Repository<T> : IRepository<T>
    {
        public Task Commit()
        {
            throw new NotImplementedException();
        }

        public Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
