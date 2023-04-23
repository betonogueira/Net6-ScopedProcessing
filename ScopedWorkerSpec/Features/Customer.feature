Feature: Customers
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Print **all** customers

Link to a feature: [Calculator](ScopedWorkerSpec/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@TestesUnitarios
Scenario: Get customer from service mapped to Cliente
	Given that a customer id is 59c0d403-71ce-4ac8-9c2c-b0e54e7c043b
	When the customer service get all customers
	Then the result should be a customer with email equals to beto@beto.com