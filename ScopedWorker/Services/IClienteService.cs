using ScopedWorker.Entities;

namespace ScopedWorker.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAll();

        Task<Cliente> GetById(Guid id);
    }
}