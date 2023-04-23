Feature: Customer
Get **customers** list, get specific customer
Link to a feature: [Customer](ScopedWorkerSpec/Features/Customer.feature)

@ClienteService
Scenario: Get customer list from service
	Given that a customer exists in the system
	When the customer service get all customers
	Then the result should be a customer list

@ClienteService
Scenario: Get customer by Id from service
	Given that a customer id is 59c0d403-71ce-4ac8-9c2c-b0e54e7c043b
	When the customer service need to get him
	Then the result should be a customer with email equals to beto@beto.com