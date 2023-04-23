using ScopedWorker.Infrastructure.Repository;
using ScopedWorker.Entities;

namespace ScopedWorker.Services;

public class ClienteService : IClienteService
{
    private readonly IRepository<Cliente> _repository;

    public ClienteService(IRepository<Cliente> repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Cliente>> GetAll()
    {
        return _repository.GetAll();
    }

    public Task<Cliente> GetById(Guid id)
    {
        return _repository.GetById(id);
    }
}
