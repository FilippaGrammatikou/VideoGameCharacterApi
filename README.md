<!-- Add the finished banner image at the path below once it exists. -->

<!-- <p align="center"><img src="docs/images/readme-banner.png" alt="VideoGameCharacterApi banner" width="100%" /></p> -->

<h1 align="center">VideoGameCharacterApi</h1>

<p align="center"><em>Focused backend learning project built to strengthen practical knowledge in ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT authentication, testing, and Dockerized local setup</em></p>

<p align="center">
  <img alt=".NET 10" src="https://img.shields.io/badge/.NET%2010-081120?style=for-the-badge&logo=dotnet&logoColor=FFD60A" />
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="EF Core" src="https://img.shields.io/badge/EF%20Core-081120?style=for-the-badge&logo=databricks&logoColor=00E5FF" />
  <img alt="SQL Server" src="https://img.shields.io/badge/SQL%20Server-0B1020?style=for-the-badge&logo=microsoftsqlserver&logoColor=FFD60A" />
  <img alt="JWT" src="https://img.shields.io/badge/JWT-081120?style=for-the-badge&logo=jsonwebtokens&logoColor=FF2DAA" />
  <img alt="Scalar" src="https://img.shields.io/badge/Scalar-0B1020?style=for-the-badge&logoColor=00E5FF" />
  <img alt="Docker" src="https://img.shields.io/badge/Docker-081120?style=for-the-badge&logo=docker&logoColor=FFD60A" />
  <img alt="xUnit" src="https://img.shields.io/badge/xUnit-0B1020?style=for-the-badge&logoColor=FF2DAA" />
</p>

<p align="center">✦ ✦ ✦ ✦ ✦</p>

---

## ◈ Overview

`VideoGameCharacterApi` is a deliberately scoped backend learning project. Instead of trying to become a large, endlessly expanding application, it focuses on strengthening practical knowledge in the backend concerns that matter most for a well-structured API: authentication, authorization, validation, query handling, predictable error responses, health checks, SQL Server persistence, test coverage, and Dockerized startup.

Its purpose is to deepen understanding of how a compact API can be structured, secured, tested, and documented with clearer technical intent.

---

## ◈ Scope

This repository is meant to demonstrate backend hardening fundamentals rather than domain complexity.

### Core focus areas

* layered API structure
* EF Core data access
* SQL Server persistence
* DTO-based contracts
* JWT authentication
* role-based authorization
* request validation
* query normalization and pagination
* centralized exception handling
* health monitoring
* Docker-based local environment
* integration and rules-focused tests

---

## ◈ Technology stack

| Area          | Technology                                 | Role                                       |
| ------------- | ------------------------------------------ | ------------------------------------------ |
| Runtime       | .NET 10                                    | application platform                       |
| API           | ASP.NET Core Web API                       | routing, middleware, controllers           |
| Data access   | Entity Framework Core                      | ORM and query composition                  |
| Database      | SQL Server                                 | relational persistence                     |
| API reference | OpenAPI + Scalar                           | endpoint inspection and manual testing     |
| Security      | JWT Bearer + authorization policies        | authentication and access control          |
| Containers    | Docker + Docker Compose                    | reproducible local setup                   |
| Testing       | xUnit + `Microsoft.AspNetCore.Mvc.Testing` | integration and rules-focused verification |

---

## ◈ Feature snapshot

| Capability         | Status      | Notes                                                             |
| ------------------ | ----------- | ----------------------------------------------------------------- |
| CRUD endpoints     | Implemented | character create/read/update/delete                               |
| Authentication     | Implemented | JWT returned from login endpoint                                  |
| Authorization      | Implemented | separate policies for read vs write access                        |
| Validation         | Implemented | data annotations on request DTOs                                  |
| Filtering          | Implemented | by `Game` and `Role`                                              |
| Sorting            | Implemented | by `Name`, `Game`, or `Role`                                      |
| Pagination         | Implemented | normalized page and capped page size                              |
| Exception handling | Implemented | centralized `ProblemDetails`-based handling                       |
| Health checks      | Implemented | `/health` endpoint                                                |
| Docker workflow    | Implemented | API + SQL Server + restore step                                   |
| Tests              | Implemented | auth, authorization, validation, smoke, success-path, query rules |

---

## ◈ Architecture at a glance

```text
Client
  ↓
Controllers
  ↓
Services
  ↓
CharacterDbContext
  ↓
SQL Server
```

| Layer          | Responsibility                                             |
| -------------- | ---------------------------------------------------------- |
| Controllers    | HTTP routing, response selection, authorization attributes |
| Services       | application logic, querying, pagination, mapping           |
| DbContext      | EF Core access to the database                             |
| DTOs           | request/response contract boundary                         |
| Infrastructure | centralized exception handling                             |
| Tests          | verification of selected API behavior                      |

For the deeper design explanation, see `docs/architecture.md`.

---

## ◈ Repository structure

```text
VideoGameCharacterApi-master/
├── Database/
│   ├── VideoGameCharactersDb.bak
│   └── restore-db.sh
├── VideoGameCharacterApi/
│   ├── Controllers/
│   ├── Data/
│   ├── Dtos/
│   ├── Infrastructure/
│   ├── Migrations/
│   ├── Models/
│   ├── Properties/
│   ├── Services/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Dockerfile
│   └── VideoGameCharacterApi.csproj
├── VideoGameCharacterApi.Tests/
├── docker-compose.yml
└── .env.example
```

---

## ◈ Security summary

Authentication is token-based and authorization is policy-based.

| Policy        | Roles allowed   | Typical use                      |
| ------------- | --------------- | -------------------------------- |
| `UserOrAdmin` | `User`, `Admin` | read endpoints                   |
| `AdminOnly`   | `Admin`         | create, update, delete endpoints |

### Demo accounts

| Role  | Username | Password   |
| ----- | -------- | ---------- |
| User  | `user`   | `user123`  |
| Admin | `admin`  | `admin123` |

> [!WARNING]
> These credentials exist for development and testing only.

For the full authentication flow, token behavior, and authorization details, see `docs/authentication.md`.

---

## ◈ Main endpoints

| Method   | Route                           | Access        | Purpose                |
| -------- | ------------------------------- | ------------- | ---------------------- |
| `POST`   | `/api/Auth/login`               | Anonymous     | issue JWT token        |
| `GET`    | `/api/VideoGameCharacters`      | User or Admin | list characters        |
| `GET`    | `/api/VideoGameCharacters/{id}` | User or Admin | retrieve one character |
| `POST`   | `/api/VideoGameCharacters`      | Admin         | create character       |
| `PUT`    | `/api/VideoGameCharacters/{id}` | Admin         | update character       |
| `DELETE` | `/api/VideoGameCharacters/{id}` | Admin         | delete character       |
| `GET`    | `/health`                       | Anonymous     | health probe           |

For the complete endpoint contract, query parameters, and request/response examples, see `docs/endpoints.md`.

---

## ◈ Quick start

### Local run

1. Configure the local SQL Server connection string in `VideoGameCharacterApi/appsettings.json`.
2. Restore dependencies.
3. Run the API project.
4. Open the local development URL.
5. Authenticate through the login endpoint and test the protected routes.

```bash
dotnet restore
dotnet run --project VideoGameCharacterApi/VideoGameCharacterApi.csproj
```

| Mode  | URL                      |
| ----- | ------------------------ |
| HTTP  | `http://localhost:5153`  |
| HTTPS | `https://localhost:7033` |

> [!NOTE]
> In development mode, the root route redirects to Scalar, and the application applies pending EF Core migrations on startup.

For the full local setup guide, see `docs/setup-local.md`.

---

## ◈ Docker quick start

The repository also supports containerized startup with SQL Server, a restore step for the database backup, and the API service.

```bash
docker compose up --build
```

| Service         | URL                            |
| --------------- | ------------------------------ |
| API             | `http://localhost:8080`        |
| Health endpoint | `http://localhost:8080/health` |

### Required environment values

| Variable                 | Purpose                  |
| ------------------------ | ------------------------ |
| `MSSQL_SA_PASSWORD`      | SQL Server SA password   |
| `JWT_KEY`                | JWT signing key          |
| `JWT_ISSUER`             | token issuer             |
| `JWT_AUDIENCE`           | token audience           |
| `ASPNETCORE_ENVIRONMENT` | ASP.NET Core environment |

Copy `.env.example` to `.env` and replace the placeholder values before starting the containers.

For the complete container workflow and restore behavior, see `docs/setup-docker.md` and `docs/configuration.md`.

---

## ◈ Testing

The test project verifies selected application behavior rather than only isolated methods.

| Test area        | Purpose                            |
| ---------------- | ---------------------------------- |
| Authentication   | valid login returns token          |
| Authorization    | missing token returns `401`        |
| Forbidden access | insufficient role returns `403`    |
| Validation       | invalid request body returns `400` |
| Success path     | valid admin request returns `201`  |
| Smoke testing    | application starts and responds    |
| Query rules      | page and page-size normalization   |

```bash
dotnet test
```

For the full testing breakdown, see `docs/testing.md`.

---

## ◈ Operational notes

| Behavior         | Current implementation                            |
| ---------------- | ------------------------------------------------- |
| Root route       | redirects to `/scalar` when Scalar is enabled     |
| OpenAPI UI       | Scalar is mapped in development                   |
| Health check     | exposed at `/health`                              |
| Database startup | pending EF Core migrations are applied at startup |
| Docker restore   | backup restore script runs before the API starts  |
| Logging          | selected auth and service events are logged       |

---

## ◈ Documentation map

To avoid duplication, deeper topics live in dedicated files:

| File                            | Purpose                                                 |
| ------------------------------- | ------------------------------------------------------- |
| `docs/architecture.md`          | internal structure and request flow                     |
| `docs/setup-local.md`           | local setup and verification                            |
| `docs/setup-docker.md`          | Docker services and restore workflow                    |
| `docs/configuration.md`         | configuration keys and environment variables            |
| `docs/authentication.md`        | login flow, JWT, and policies                           |
| `docs/endpoints.md`             | endpoint reference and examples                         |
| `docs/testing.md`               | test structure and execution                            |
| `docs/errors-and-validation.md` | validation, status codes, and `ProblemDetails` behavior |
