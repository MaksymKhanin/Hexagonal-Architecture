.Net 5 
Tests: SpecFlow (SendInvoiceStepsShould), IntegrationTests (SendInvoiceTestsShould), UnitTests
Azure Service Bus, Azure CosmosDB, Azure Function, Mediator, Mapper

1) Setup cosmos db emulator with URL http://localhost:8081
2) In cosmos db emulator create database with name "ReadDb" and 2 containers inside: "ReadContainer" and "WriteContainer"; 
   and db with name: "WriteDb" with 2 containers: "ReadContainer" and "WriteContainer"
3) Setup environment variable on machine with 
   name: COSMOS_EMULATOR_CONNECTION_STRING 
   value: AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==
4) Setup Azure service bus with topic "mytopic". Add Service bus connection string into appsettings.dev.json 
5) Send request with payload:
{
  "number": "#12345678",
  "amount": 67
}

6) All tests pass! Ncrunch test may not be shomn in NCrunch explorer! But in Test explorer is shown well.