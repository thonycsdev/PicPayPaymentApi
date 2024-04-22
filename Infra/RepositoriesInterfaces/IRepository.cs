namespace Infra.RepositoriesInterfaces
{
    public interface IRepository<T>
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task<T> Update(T entity);
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task Commit();
    }
}
