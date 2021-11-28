# AbraFlexiAspNetCore
ASP.NET Core Library for interaction with the Czech accounting system Abra Flexi.

## Installation
### Install the Nuget package
You can add the package to your project like this:
`dotnet add package AbraFlexiAspNetCore --version 2.0.0`
### Add the service to the dependency injection
Add the service to the service collection in the ConfigueServices method in **Startup.cs**:
`services.AddAbraFlexi("my-company.flexibee.eu", "company_identifier", "username", "password");`
## Usage
After you've added the Abra service to the dependency injection, you can get the client instance by adding this to your constructors:
`IAbraFlexiClient abraFlexiClient`
