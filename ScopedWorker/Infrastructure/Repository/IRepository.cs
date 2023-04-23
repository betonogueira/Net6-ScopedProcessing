namespace ScopedWorker.Infrastructure.Repository;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAll();

    public Task<T> GetById(Guid id);
}
