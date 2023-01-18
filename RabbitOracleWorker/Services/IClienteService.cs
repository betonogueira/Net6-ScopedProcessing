using RabbitOracleWorker.Entities;

namespace RabbitOracleWorker.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodos();
    }
}