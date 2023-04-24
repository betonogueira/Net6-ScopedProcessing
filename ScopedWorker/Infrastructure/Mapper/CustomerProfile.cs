using ScopedWorker.Domain.Entities;
using ScopedWorker.Services.Dto;
using AutoMapper;

namespace ScopedWorker.Infrastructure.Mapper;

public class CustomerProfile : Profile
{
    public CustomerProfile() 
    {
        CreateMap<Task<Customer>, Task<CustomerDto>>();
        CreateMap<Task<IEnumerable<Customer>>, Task<IEnumerable<CustomerDto>>>();
    }
}
