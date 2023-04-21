using ScopedWorker.Entities;

namespace ScopedWorker.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodos();
    }
}