using RabbitOracleWorker.Entities;
using RabbitOracleWorker.Infrastructure.Repository;

namespace RabbitOracleWorker.Services;

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
