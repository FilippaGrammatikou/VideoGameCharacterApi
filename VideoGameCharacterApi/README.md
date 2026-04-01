<div align="center">

# VideoGameCharacterApi

### ASP.NET Core Web API practice project built around a simple video game character domain

![C#](https://img.shields.io/badge/C%23-A7D8FF?style=for-the-badge&logo=csharp&logoColor=000000)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-A7D8FF?style=for-the-badge&logo=dotnet&logoColor=000000)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-A7D8FF?style=for-the-badge&logoColor=000000)
![SQL Server](https://img.shields.io/badge/SQL%20Server-A7D8FF?style=for-the-badge&logo=microsoftsqlserver&logoColor=000000)
![OpenAPI](https://img.shields.io/badge/OpenAPI-A7D8FF?style=for-the-badge&logo=openapiinitiative&logoColor=000000)
![Scalar](https://img.shields.io/badge/Scalar-A7D8FF?style=for-the-badge&logoColor=000000)

</div>

---

## Overview

`VideoGameCharacterApi` is a small database-backed ASP.NET Core Web API created to practice CRUD-oriented backend development in .NET.

The project is centered on a simple video game character domain and explores how a compact API can be structured using controllers, services, DTOs, Entity Framework Core, dependency injection, and migrations.

---

## Getting started

To run the project locally, make sure the following are available:

| **Requirement** | **Purpose** |
|---|---|
| **.NET SDK** | build and run the API |
| **SQL Server / LocalDB** | local database engine |
| **EF Core tools** | apply migrations from the command line |

### 1. Configure the database connection

Open `appsettings.json` and update the SQL Server connection string so it matches your local environment.

### 2. Restore project dependencies

```bash
dotnet restore
```

### 3. Apply the migrations

```bash
dotnet ef database update
```

This will create or update the local database using the migrations included in the project.

### 4. Run the API

```bash
dotnet run
```

### 5. Test the API

Once the application is running, open the API through the configured OpenAPI / Scalar setup or send requests to:

`http://localhost:5153`

You can also test the endpoints with Postman.

---

## Core technologies

| **Technology** | **Role in the project** |
|---|---|
| **ASP.NET Core** | Web API framework and HTTP pipeline |
| **Entity Framework Core** | ORM and data access |
| **SQL Server** | relational database |
| **DTOs** | request and response shaping |
| **Service layer** | separation of controller and data/business logic |
| **OpenAPI / Scalar** | endpoint inspection and API exploration |

---

## API scope

The current API supports the core CRUD workflow for video game characters:

- retrieve all characters
- retrieve a character by ID
- create a new character
- update an existing character
- delete a character

---

## Repository structure

| **Folder** | **Responsibility** |
|---|---|
| `Controllers` | endpoint definitions and HTTP handling |
| `Data` | EF Core database context |
| `Dtos` | request and response models |
| `Models` | domain entities |
| `Services` | service contracts and implementations |
| `Migrations` | EF Core migration files |

---

## Purpose

This repository is intended as a learning-stage backend project rather than a finished production system. Its role is to strengthen practical understanding of ASP.NET Core Web APIs, layered application structure, EF Core with SQL Server, and DTO-based CRUD design.
