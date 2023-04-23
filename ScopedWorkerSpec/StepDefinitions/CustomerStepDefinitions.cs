using ScopedWorker.Entities;
using ScopedWorker.Services;
using Moq;

namespace ScopedWorkerSpec.StepDefinitions;

[Binding]
public sealed class CustomerStepDefinitions
{
    private readonly Mock<IClienteService> _clienteService;
    private Guid _customerId;
    private IEnumerable<Cliente> _customerList;
    private Cliente _customer;
    private Cliente? _customerResult;
    private IEnumerable<Cliente>? _customerListResult;

    public CustomerStepDefinitions()
    {
        _customer = new Cliente();
        _customerList = new List<Cliente>() { _customer };
        _clienteService = new Mock<IClienteService>();
        _clienteService.Setup(svc => svc.GetAll()).Returns(Task.FromResult(_customerList));
        _clienteService.Setup(svc => svc.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(_customer));
    }

    #region Get customer list from service

    [Given("that a customer exists in the system")]
    public void GivenThereIsCustomerSubscribedToTheSystem()
    {
        _customerId = Guid.Parse("59c0d403-71ce-4ac8-9c2c-b0e54e7c043b");
        _customer.Id = _customerId;
        _customer.Name = "Test 01";
        _customer.Email = "joe@doe.com";
    }

    [When("the customer service get all customers")]
    public void WhenTheCustomerServiceGetAllCustomers()
    {
        var clientes = _clienteService.Object.GetAll();
        _customerListResult = clientes.Result;
    }

    [Then("the result should be a customer list")]
    public void ThenTheResultShouldBe()
    {
        _customerListResult.Should()
            .NotBeNull()
            .And.BeAssignableTo<IEnumerable<Cliente>>();
    }

    #endregion

    #region Get customer by Id from service

    [Given("that a customer id is (.*)")]
    public void GivenTheCustomerIdIs(Guid id)
    {
        _customerId = id;
        _customer.Id = _customerId;
        _customer.Name = "Test 02";
        _customer.Email = "beto@beto.com";
    }

    [When("the customer service need to get him")]
    public void WhenTheCustomerServiceGetByIdIsCalled()
    {
        var cliente = _clienteService.Object.GetById(_customerId);
        _customerResult = cliente.Result;
    }

    [Then("the result should be a customer with email equals to (.*)")]
    public void ThenTheResultShouldBeOneCustomerWithEmailEqualsTo(string result) => _customerResult.Email.Should().Be(result);
 
    #endregion
}