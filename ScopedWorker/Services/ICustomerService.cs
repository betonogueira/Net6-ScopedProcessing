using ScopedWorker.Services.Dto;

namespace ScopedWorker.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAll();

        Task<CustomerDto> GetById(Guid id);
    }
}