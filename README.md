# Store API

## Overview

This project implements an API for managing sales records in a Developer Store.

[See more](/.doc/overview.md)

## Key Features

* CRUD operations for sales records
* Discount calculation based on quantity

## Getting Started

### Prerequisites

* [.NET SDK 8.0 or later](https://dotnet.microsoft.com/en-us/download)
* [Docker](https://www.docker.com/get-started/) 

### Configuration

1.  Clone the repository.
1.  Set up the database using `docker-compose.yml`, [see infrastructure setup guide](/.doc/setup-infrastructure.md).
1.  Configure any necessary settings in `appsettings.json`.

### Running the API

```bash
dotnet run --project Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
```

### Running Tests

```bash
dotnet test Ambev.DeveloperEvaluation.sln
```
### Test Coverage

Learn how to run code coverage analysis and generate HTML reports to identify untested parts of the system.

[See more](/.doc/code-coverage.md)

## Code Conventions

This project adheres to the following code conventions to ensure consistency, readability, and maintainability:

* **Git Flow:** We follow the Git Flow workflow for branch management. This provides a robust framework for managing feature development, releases, and hotfixes. For a comprehensive guide, please refer to the [Git Flow documentation](https://datasift.github.io/gitflow/OpenSourceProjectWorkflow.html).
* **Semantic Commits:** Commit messages are structured using Semantic Commits. This convention helps to automate versioning, generate changelogs, and improve the clarity of the commit history.  More details can be found in the [Semantic Versioning specification](https://semver.org/).
* **Migrations in ORM Layer:** Entity Framework Core Migrations are stored within the ORM layer (`src/Ambev.DeveloperEvaluation.ORM/Migrations`). This keeps database schema management closely aligned with the data access logic.
* **C# Coding Standards:**
    * PascalCase is used for class and method names (e.g., `UserService`, `GetUserName`).
    * camelCase is used for variable and parameter names (e.g., `userName`, `productId`).
* **Clean Architecture/Domain-Driven Design (DDD):** The project structure is organized based on Clean Architecture and Domain-Driven Design principles. This promotes separation of concerns, testability, and maintainability.
* **Dependency Injection:** Dependency Injection (DI) is used extensively to achieve Inversion of Control (IoC). This enhances flexibility, decouples components, and simplifies unit testing.
* **Design Patterns:** Common design patterns such as Repository, Unit of Work, and others are employed where appropriate to solve recurring design problems and improve code quality.

## Tech Stack
This section lists the key technologies used in the project, including the backend, testing, frontend, and database components. 

See [Tech Stack](/.doc/tech-stack.md)

## Frameworks
This section outlines the frameworks and libraries that are leveraged in the project to enhance development productivity and maintainability. 

See [Frameworks](/.doc/frameworks.md)

## API Structure
This section includes links to the detailed documentation for the different API resources:
- [API General](./docs/general-api.md)
- [Products API](/.doc/products-api.md)
- [Carts API](/.doc/carts-api.md)
- [Users API](/.doc/users-api.md)
- [Auth API](/.doc/auth-api.md)
- [Authorization Policies](/.doc/authorization.md)


## Advanced Filtering
Learn how the API supports dynamic filtering, ordering, and pagination for cleaner queries and extensible backends.

See [Advanced Filtering Guide](/.doc/advanced-filtering.md)

## Project Structure
This section describes the overall structure and organization of the project files and directories. 

See [Project Structure](/.doc/project-structure.md)

## Contact

[Willian Lima](https://www.linkedin.com/in/w-lima)

[![Perfil do LinkedIn](https://media.licdn.com/dms/image/v2/D4D03AQGRObzA0_NRkg/profile-displayphoto-shrink_200_200/profile-displayphoto-shrink_200_200/0/1703104875697?e=1751500800&v=beta&t=jWwem7-YUYxBoktc3ayzIMLMdT4RlMQcsh-WlFW0pTM)](https://www.linkedin.com/in/w-lima)