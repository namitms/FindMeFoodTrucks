# Issue register

1. Cosmos DB query strings are not validated for SQL injection attacks.Refer -  https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos.querydefinition.withparameter?view=azure-dotnet 
2. Swagger is not enabled to support reading API key from header
3. Azure Keyvault is not integrated to the solution. All production secrets are stored in Application configuration of Azure Functions and Web Apps
1. Azure App Gateway and Azure API management layers are not introduced
1. Infrastructure provisioning (Infrastructure as Code) is not implemented
1. Integration tests are not included in the test suite