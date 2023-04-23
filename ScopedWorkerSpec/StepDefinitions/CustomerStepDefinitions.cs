using ScopedWorker.Entities;
using ScopedWorker.Services;
using Moq;
using NuGet.Frameworks;

namespace ScopedWorkerSpec.StepDefinitions;

[Binding]
public sealed class CustomerStepDefinitions
{
    private readonly Mock<IClienteService> _clienteService;
    private Guid _customerId;
    private IEnumerable<Cliente> _customerList;
    private Cliente _customer;
    private Cliente? _customerResult;

    public CustomerStepDefinitions()
    {
        _customer = new Cliente();
        _customerList = new List<Cliente>() { _customer };
        _clienteService = new Mock<IClienteService>();
        _clienteService.Setup(svc => svc.ObterTodos()).Returns(Task.FromResult(_customerList));   
    }

    [Given("that a customer id is (.*)")]
    public void GivenTheCustomerIdIs(Guid id)
    {
        _customerId = id;
        _customer.Id = _customerId;
        _customer.Name = "Test";
        _customer.Email = "beto@beto.com";
    }

    [When("the customer service get all customers")]
    public void WhenTheCustomerServiceGetAllCustomers()
    {
        var clientes = _clienteService.Object.ObterTodos();
        _customerResult = clientes.Result.First(x => x.Id == _customerId);
    }

    [Then("the result should be a customer with email equals to (.*)")]
    public void ThenTheResultShouldBe(string result) => _customerResult.Email.Should().Be(result);
}