# Architecture

> Internal structure and request flow for `VideoGameCharacterApi`.

<!-- Optional visual header -->

<!-- ![Architecture banner](images/architecture-banner.png) -->

---

## ◈ Purpose

This document explains how the API is structured internally and why its components are separated the way they are.

---

## ◈ Architectural intent

`VideoGameCharacterApi` is deliberately small in domain scope and more serious in backend structure.

The project is designed to strengthen practical knowledge around:

* separating HTTP concerns from application logic
* keeping DTOs distinct from EF Core entities
* shaping queries through filtering, sorting, and pagination
* using role-based authorization
* returning predictable error responses
* applying database migrations at startup
* testing both application rules and HTTP behavior

---

## ◈ High-level structure

```text
Client
  ↓ HTTP request
ASP.NET Core middleware pipeline
  ↓
Controllers
  ↓
Services
  ↓
CharacterDbContext
  ↓
SQL Server
```

### Architectural view

```text
┌──────────────────────────────┐
│            Client            │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│   Authentication / AuthZ     │
│ Exception handling / Routing │
│  OpenAPI / Scalar / Health   │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│         Controllers          │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│           Services           │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│      CharacterDbContext      │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│         SQL Server           │
└──────────────────────────────┘
```

<!-- Optional later visual -->

<!-- ![Architecture diagram](images/architecture-diagram.png) -->

---

## ◈ Project structure

```text
VideoGameCharacterApi-master/
├── Database/
│   ├── VideoGameCharactersDb.bak
│   └── restore-db.sh
├── VideoGameCharacterApi/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   └── VideoGameCharactersController.cs
│   ├── Data/
│   │   └── CharacterDbContext.cs
│   ├── Dtos/
│   ├── Infrastructure/
│   │   └── GlobalExceptionHandler.cs
│   ├── Migrations/
│   ├── Models/
│   │   └── Character.cs
│   ├── Services/
│   │   ├── IVideoGameCharacterService.cs
│   │   ├── QueryRules.cs
│   │   └── VideoGameService.cs
│   ├── Program.cs
│   └── VideoGameCharacterApi.csproj
├── VideoGameCharacterApi.Tests/
└── docker-compose.yml
```

---

## ◈ Layer responsibilities

| Layer               | Main elements                                                  | Responsibility                                                                            | What it should not own                                        |
| ------------------- | -------------------------------------------------------------- | ----------------------------------------------------------------------------------------- | ------------------------------------------------------------- |
| Controllers         | `AuthController`, `VideoGameCharactersController`              | accept HTTP requests, apply routing and authorization attributes, select HTTP responses   | direct EF Core access, query composition details              |
| Services            | `IVideoGameCharacterService`, `VideoGameService`, `QueryRules` | application logic, query shaping, mapping, pagination rules                               | HTTP routing concerns, middleware concerns                    |
| Data                | `CharacterDbContext`                                           | EF Core access to the database, model configuration, index configuration                  | HTTP semantics, token generation                              |
| Models              | `Character`                                                    | persistence model used by EF Core                                                         | request validation attributes intended only for API contracts |
| DTOs                | request, response, login, query, paging DTOs                   | define the API contract boundary                                                          | database tracking behavior                                    |
| Infrastructure      | `GlobalExceptionHandler`                                       | normalize unexpected failures into predictable server responses                           | domain logic or CRUD operations                               |
| Startup composition | `Program.cs`                                                   | register services, configure middleware, wire authentication/authorization, map endpoints | business logic                                                |
| Tests               | integration tests and query rule tests                         | verify endpoint behavior and small isolated rules                                         | production request handling                                   |

---

## ◈ Why the layers are separated

### Controllers

Controllers are intentionally thin. Their job is to represent the HTTP surface of the API:

* route matching
* request binding
* authorization attributes
* response selection such as `Ok`, `CreatedAtAction`, `NoContent`, or `Problem`

This keeps controller code readable and prevents HTTP details from being mixed with query logic.

### Services

Services hold the application-side work that should not live in controllers:

* filtering
* sorting
* pagination
* DTO projection
* add/update/delete behavior
* logging around domain actions

This gives the API one place where data access behavior can evolve without forcing controllers to become large.

### DTOs vs entities

DTOs and EF Core entities are separated so the public API contract does not become identical to the persistence model.

That separation helps with:

* validation on request models
* response shaping
* avoiding accidental overexposure of entity structure
* keeping database concerns distinct from HTTP contract concerns

### DbContext

`CharacterDbContext` is the EF Core gateway to the database. It represents the unit that knows how to query and persist entities, and it also contains model-level configuration such as the index on `Game`.

### Infrastructure

Unexpected failures are handled through a dedicated global exception handler so that server errors remain predictable and structured instead of becoming ad hoc response text.

---

## ◈ Core components

| Component                                           | Role in the system                                                                      |
| --------------------------------------------------- | --------------------------------------------------------------------------------------- |
| `Program.cs`                                        | application composition root; registers services and configures the middleware pipeline |
| `AuthController`                                    | development/demo login endpoint that issues JWT tokens                                  |
| `VideoGameCharactersController`                     | HTTP endpoints for character retrieval and mutation                                     |
| `IVideoGameCharacterService`                        | service contract used by controllers                                                    |
| `VideoGameService`                                  | main application service for CRUD operations and list-query shaping                     |
| `QueryRules`                                        | small rule utility for page and page-size normalization                                 |
| `CharacterDbContext`                                | EF Core database access and model configuration                                         |
| `GlobalExceptionHandler`                            | centralized 500-response handling with `ProblemDetails`                                 |
| `Character`                                         | EF Core entity persisted in SQL Server                                                  |
| `CreateCharacterRequest` / `UpdateCharacterRequest` | incoming write contracts                                                                |
| `CharacterResponseDto`                              | outgoing character representation                                                       |
| `GetCharactersQuery`                                | list query options for filtering, sorting, and pagination                               |
| `PagedResponseDto<T>`                               | wraps list results with paging metadata                                                 |

---

## ◈ Request lifecycle

The main list endpoint is a good example of how the layers cooperate.

### Example flow: `GET /api/VideoGameCharacters`

```text
1. Client sends HTTP request
2. ASP.NET Core routes request to VideoGameCharactersController
3. Authorization policy is evaluated
4. Query values are bound into GetCharactersQuery
5. Controller calls IVideoGameCharacterService
6. Service builds EF Core query
7. Filters, sorting, and pagination are applied
8. Entities are projected into CharacterResponseDto
9. SQL query is executed
10. PagedResponseDto is returned
11. Controller returns 200 OK with JSON payload
```

### Flow table

| Stage              | Responsible part               | Outcome                                           |
| ------------------ | ------------------------------ | ------------------------------------------------- |
| Request entry      | middleware pipeline            | request reaches routing/auth/error infrastructure |
| Endpoint selection | controller routing             | correct action is selected                        |
| Access check       | authorization policy           | request is allowed or blocked                     |
| Application work   | service layer                  | query or mutation logic runs                      |
| Persistence work   | `CharacterDbContext` + EF Core | database is queried or updated                    |
| Response shaping   | service + controller           | DTOs and HTTP status code are returned            |

---

## ◈ Read path design

The list endpoint uses a read-oriented query shape.

### Notable decisions

| Design choice                     | Why it exists                                                           |
| --------------------------------- | ----------------------------------------------------------------------- |
| `AsNoTracking()` on list reads    | avoids tracking overhead for read-only queries                          |
| `AsQueryable()`                   | allows filters, sorting, and pagination to be composed before execution |
| DTO projection with `Select(...)` | returns only the data required by the API contract                      |
| page normalization                | prevents invalid page numbers such as `0` or negatives                  |
| page-size normalization and cap   | prevents invalid sizes and discourages overly large result sets         |
| fallback sort by `Id`             | ensures stable ordering when no supported sort field is provided        |

This keeps the read path explicit, bounded, and easier to explain.

---

## ◈ Write path design

Create, update, and delete operations are admin-only and follow a simpler command-style flow.

```text
Controller receives request
  ↓
Service validates target existence where needed
  ↓
Entity is created / modified / removed
  ↓
DbContext.SaveChangesAsync()
  ↓
Controller returns Created / NoContent / Problem response
```

### Write-operation characteristics

| Operation type | Architectural behavior                                    |
| -------------- | --------------------------------------------------------- |
| Create         | request DTO is mapped into a new entity and persisted     |
| Update         | target entity is loaded, mutated, and saved               |
| Delete         | target entity is loaded, removed, and saved               |
| Logging        | significant write actions are logged in the service layer |

---

## ◈ Authentication and authorization placement

Authentication and authorization are configured in `Program.cs` and enforced before protected controller actions execute.

### Structural role of each part

| Part                     | Responsibility                                 |
| ------------------------ | ---------------------------------------------- |
| JWT bearer configuration | defines how incoming tokens are validated      |
| authorization policies   | define which roles may access which actions    |
| controller attributes    | apply those access rules to specific endpoints |
| auth controller          | issues tokens for the demo login flow          |

This document only describes placement and structure.
For the full token flow, demo credentials, and policy behavior, see `authentication.md`.

---

## ◈ Middleware flow

The request pipeline is assembled in `Program.cs`.

### Conceptual order

```text
Application start
  ↓
Service registration
  ↓
Database migration on startup
  ↓
Development-only OpenAPI / Scalar mapping
  ↓
Root redirect mapping
  ↓
Health checks mapping
  ↓
Exception handler middleware
  ↓
Authentication middleware
  ↓
Authorization middleware
  ↓
Controller endpoint mapping
```

### Middleware responsibilities

| Middleware / mapping     | Purpose                                                     |
| ------------------------ | ----------------------------------------------------------- |
| OpenAPI + Scalar mapping | exposes interactive API documentation in development        |
| root redirect            | sends `/` to `/scalar`                                      |
| health checks            | exposes `/health`                                           |
| exception handler        | converts unhandled exceptions into structured 500 responses |
| authentication           | validates bearer tokens                                     |
| authorization            | enforces role/policy access rules                           |
| controller mapping       | activates controller endpoints                              |

---

## ◈ Error-handling structure

The architecture distinguishes between **expected API outcomes** and **unexpected server failures**.

### Expected outcomes

These are handled directly by controller or service logic, for example:

* `404 Not Found` when a character does not exist
* `401 Unauthorized` when login fails or a token is invalid
* `403 Forbidden` when a valid user lacks the required role
* `400 Bad Request` when request validation fails

### Unexpected outcomes

Unhandled exceptions are passed to `GlobalExceptionHandler`, which returns a structured `ProblemDetails` response with a `traceId` for correlation.

This gives the API a cleaner failure boundary and avoids random unstructured server responses.

For the full error catalog and validation behavior, see `errors-and-validation.md`.

---

## ◈ Data model and persistence

The domain model is intentionally compact.

### Entity

| Entity      | Main fields                  |
| ----------- | ---------------------------- |
| `Character` | `Id`, `Name`, `Game`, `Role` |

### Persistence decisions

| Decision                      | Reason                                                |
| ----------------------------- | ----------------------------------------------------- |
| SQL Server backend            | relational persistence for the API                    |
| EF Core DbContext             | query and persistence abstraction                     |
| migration files committed     | schema evolution is versioned in source control       |
| index on `Game`               | supports the filtered read path more intentionally    |
| startup migration application | reduces setup friction when the database is reachable |

---

## ◈ Startup and deployment shape

Although full setup instructions belong elsewhere, the architectural shape includes two runtime paths:

| Runtime path             | Architectural significance                                                 |
| ------------------------ | -------------------------------------------------------------------------- |
| Local execution          | app starts, applies migrations, exposes Scalar and health checks           |
| Docker Compose execution | API, SQL Server, and restore workflow run as separate cooperating services |

The backup restore script and compose configuration belong to the deployment story of the project rather than the controller/service architecture itself.

For exact commands and environment configuration, see `setup-local.md`, `setup-docker.md`, and `configuration.md`.

---

## ◈ Testing architecture

The test project complements the layered structure in two ways:

| Test type                        | Role                                                                                |
| -------------------------------- | ----------------------------------------------------------------------------------- |
| endpoint/integration-style tests | verify authentication, authorization, validation, and success/failure HTTP behavior |
| focused rules tests              | verify small isolated logic such as query normalization                             |

This matches the architecture well: HTTP behavior is tested at the application boundary, while simple rule logic is tested more directly.

For test execution and test-file coverage details, see `testing.md`.

---

## ◈ Related documentation

| File                       | Purpose                                         |
| -------------------------- | ----------------------------------------------- |
| `../README.md`             | repository overview and quick-start summary     |
| `authentication.md`        | JWT flow, access policies, and login usage      |
| `endpoints.md`             | endpoint contract and request/response examples |
| `setup-local.md`           | local setup and verification                    |
| `setup-docker.md`          | container workflow and restore behavior         |
| `configuration.md`         | environment variables and configuration keys    |
| `testing.md`               | test structure and execution                    |
| `errors-and-validation.md` | validation behavior and failure responses       |
