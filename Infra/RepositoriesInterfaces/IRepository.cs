namespace Infra.RepositoriesInterfaces
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        void Delete(T entity);
        T Update(T entity);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task Commit();
    }
}
