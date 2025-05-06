[Back to README](../README.md)

## Frameworks
Our frameworks are the building blocks that enable us to create robust, efficient, and maintainable software solutions. They have been carefully selected to complement our tech stack and address specific development challenges we face in our projects.

These frameworks enhance our development process by providing tried-and-tested solutions to common problems, allowing our team to focus on building unique features and business logic. Each framework has been chosen for its ability to integrate seamlessly with our tech stack, its community support, and its alignment with our development principles.

We use the following frameworks in this project:

Backend:
- **AutoMapper**: A convention-based object-object mapper that simplifies the process of mapping between different object types.
  - Git: https://github.com/AutoMapper/AutoMapper
- **BCrypt.Net-Next**: A library for password hashing using the bcrypt algorithm, focused on security and ease of use.
  - Git: https://github.com/BcryptNet/bcrypt.net
- **MediatR**: A behavioral design pattern that helps reduce chaotic dependencies between objects. It allows loose coupling by encapsulating object interaction.
  - Git: https://github.com/jbogard/MediatR
- **OneOf**: A library that enables the use of discriminated unions in C#, making it easier to handle multiple return types in a single type.
  - Git: https://github.com/mcintyre321/OneOf
- **Serilog**: A structured logging library for .NET, with support for various sinks (log destinations) and enrichers (addition of contextual information to logs). We utilize integrations with ASP.NET Core and sinks like Console, along with enrichers for environment information, exception data, and support for Entity Framework Core.
  - Git: https://github.com/serilog/serilog

Database:
- **EF Core**: Entity Framework Core, a lightweight, extensible, and cross-platform version of Entity Framework, used for data access and object-relational mapping.
  - Git: https://github.com/dotnet/efcore
- **Microsoft.EntityFrameworkCore**: Base package for Entity Framework Core.
  - Git: https://github.com/dotnet/efcore
- **Microsoft.EntityFrameworkCore.Design**: Contains design-time tools for Entity Framework Core, such as migrations.
  - Git: https://github.com/dotnet/efcore
- **Microsoft.EntityFrameworkCore.Relational**: Common relational abstractions for Entity Framework Core.
  - Git: https://github.com/dotnet/efcore
- **Npgsql.EntityFrameworkCore.PostgreSQL**: Entity Framework Core provider for the PostgreSQL database.
  - Git: https://github.com/npgsql/efcore.pg

Testing:
- **Bogus**: A library for generating fake data for testing purposes, allowing for more realistic and diverse test scenarios.
  - Git: https://github.com/bchavez/Bogus
- **coverlet.collector**: A data collector for Coverlet that generates code coverage information for unit tests.
  - Git: https://github.com/coverlet-coverage/coverlet
- **coverlet.msbuild**: MSBuild integration for Coverlet to collect code coverage information during the project build.
  - Git: https://github.com/coverlet-coverage/coverlet
- **FluentAssertions**: A library that provides a set of fluently chainable assertion methods for .NET tests.
  - Git: https://github.com/fluentassertions/fluentassertions
- **NSubstitute**: A friendly substitute for .NET mocking libraries, used for creating test doubles in unit testing.
  - Git: https://github.com/nsubstitute/NSubstitute
- **xunit**: A modern unit testing framework for .NET, focused on extensibility and clarity.
  - Git: https://github.com/xunit/xunit
- **FluentValidation**: A library for building strongly-typed and fluent validation rules for .NET objects.
  - Git: https://github.com/FluentValidation/FluentValidation

Tools:
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets**: Tools for Visual Studio that facilitate the development of .NET applications in Docker containers.
  - Git: https://github.com/microsoft/vsts-docker
- **Roslynator.Analyzers**: A set of Roslyn code analyzers that enforce code style and best practices.
  - Git: https://github.com/JosefPihrt/Roslynator
- **Roslynator.Testing.CSharp.Xunit**: Library to assist in testing Roslyn analyzers using xUnit.net.
  - Git: https://github.com/JosefPihrt/Roslynator

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./tech-stack.md">Previous: Tech Stack</a>
  <a href="./general-api.md">Next: General API</a>
</div>