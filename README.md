# TemplateOnionAPI
Blog - [Get started with the Visual Studio template OnionAPI to scaffold ASP.NET WebAPI project for .NET 7](https://medium.com/scrum-and-coke/get-started-with-the-visual-studio-template-onionapi-to-scaffold-asp-net-webapi-project-for-net-7-558a661cff5)

Developers can use the Visual Studio template OnionAPI to scaffold a clean architecture REST API solution consisting of five projects

1. Domain - entities and the common models
2. Application - Interfaces, CQRS Features, Exceptions, Behaviors
3. Infrastructure.Persistence - application-specific database access
4. Infrastructure.Shared - common services such as Mail Service, Date Time Service, Mock, and so on
5. WebApi - controllers for REST API resources and endpoints

The underline tech stack provides loosely-coupled and inverted-dependency architecture with good design patterns and practices.

1. ASP.NET CORE 7 — a framework for creating RESTful services, also known as web APIs, using C#
2. Repository Pattern — abstraction layer between the data access layer and the controller
3. CQRS (Command and Query Responsibility Segregation) Pattern — separating read and update operations for a data store to maximize performance, scalability, and security based on MediatR and AutoMapper
4. Entity Framework Core — a lightweight, extensible, open-source, and cross-platform version of the popular Entity Framework data access technology
5. Swashbuckle — the industry standard for REST API documentation and testing
6. Bogus — a realistic, easy to use mock data .NET library for rapid REST API design and testing

# How to install the template during development
Copy the TemplateOnionAPI.zip (in the root of the repo) to the directory of the Visual Studio templates for C# on your desktop.

For Visual Studio 2019, the template folder is at 

C:\Users\[UserName]\Documents\Visual Studio 2019\Templates\ProjectTemplates\Visual C#

Reference: [How to: Create multi-project templates](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-create-multi-project-templates?view=vs-2019)


