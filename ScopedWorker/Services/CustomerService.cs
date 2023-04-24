using ScopedWorker.Infrastructure.Repository;
using ScopedWorker.Domain.Entities;
using ScopedWorker.Services.Dto;
using AutoMapper;

namespace ScopedWorker.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;
    
    public CustomerService(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<IEnumerable<CustomerDto>> GetAll()
    {
        return _mapper.Map<Task<IEnumerable<CustomerDto>>>(_repository.GetAll());
    }

    public Task<CustomerDto> GetById(Guid id)
    {
        return _mapper.Map<Task<CustomerDto>>(_repository.GetById(id));
    }
}
