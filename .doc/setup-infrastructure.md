# 🛠️ Infrastructure Setup Guide

This document provides step-by-step instructions to configure the local development infrastructure, including Docker setup, tool installation, and database migration management.

---

## 📦 Prerequisites

Make sure the following tools are installed before proceeding:

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- EF Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

⸻

🐳 Starting Infrastructure with Docker

This project includes a docker-compose.develop.yml file to provision required infrastructure services (such as PostgreSQL) for local development and testing.

To start the infrastructure services, run:

```bash
docker compose -f docker-compose.develop.yml up -d
```

✅ Make sure port 5432 (PostgreSQL) is available before running the command.

⸻

🚀 First Application Run

When you run the application for the first time (dotnet run or through your IDE), it will:
1.	Automatically apply any pending database migrations.
2.	Check if an admin user exists.
3.	If not, it will create the following default admin user:

- Username: developer.evaluation
- Email: developer.evaluation@test.com
- Password: Test@1234 (hashed)
- Role: Admin

🔐 Passwords are securely hashed using the built-in IPasswordHasher.

This is handled inside the InfrastructureModuleInitializer.ApplyMigrate() method and only runs when not in a Test environment.

⸻

🗃️ Creating Migrations

When modifying the database structure, generate a new migration with the command:
```bash
dotnet ef migrations add YourMigrationName \
  --context DefaultContext \
  --output-dir "Migrations" \
  --project "src/Ambev.DeveloperEvaluation.ORM" \
  --startup-project "src/Ambev.DeveloperEvaluation.ORM"
```

Example names: FirstRelease, AddNewFieldToUser, UpdateProductSchema

⚠️ You don’t need to manually run dotnet ef database update, since the app does this automatically on startup.

⸻

✅ Developer Setup Checklist
* Install .NET SDK 8+
* Install Docker
* Install EF Core CLI
* Start infrastructure:

docker compose -f docker-compose.develop.yml up -d

* Run the application:

dotnet run --project src/Ambev.DeveloperEvaluation.WebApi

* Confirm that database and admin user are created automatically