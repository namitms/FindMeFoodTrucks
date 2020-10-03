# Solution Architecture
The following diagram illustrates the high-level solution architecture

![Image](/Images/Architecture.png)

* API Gateway will be implement using Azure API Management service
* Web APIs will be hosted on Azure App Services
* Cosmos DB will be used as the data store
* Ingestion code will be hosted on Azure Functions triggered once a day using scheduled triggers
* Application insights will be used for application logging and monitoring
* Log Analytics will be used as the monitoring control plane
* GitHub will be used as a source repository, for CI/CD and for project management (Kanban board using GitHub Projects)
* Visual Studio will be used for development
* The development framework for business login and ingestor will be .net core and the programming language will be C#
* X Unit will be used to perform Unit testing
* Swagger will be used for API documentation
* Azure key vault will be used as a secret store
