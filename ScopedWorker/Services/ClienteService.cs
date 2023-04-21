using ScopedWorker.Entities;
using ScopedWorker.Infrastructure.Repository;

namespace ScopedWorker.Services;

public class ClienteService : IClienteService
{
    private readonly IRepository<Cliente> _repository;

    public ClienteService(IRepository<Cliente> repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Cliente>> ObterTodos()
    {
        return _repository.GetAll();
    }
}
