<h1 align="center">Architecture</h1>

<p align="center"><em>Application structure and request flow for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="EF Core" src="https://img.shields.io/badge/EF%20Core-081120?style=for-the-badge&logoColor=00E5FF" />
  <img alt="SQL Server" src="https://img.shields.io/badge/SQL%20Server-0B1020?style=for-the-badge&logo=microsoftsqlserver&logoColor=FFD60A" />
</p>

---

## Overview

`VideoGameCharacterApi` uses a layered ASP.NET Core Web API structure that separates HTTP handling, application logic, and persistence. Controllers receive requests, services execute application behavior, and Entity Framework Core reaches SQL Server through `CharacterDbContext`.

## High-Level Flow

```mermaid
flowchart LR
    A[Client / Scalar / API Consumer] --> B[Controllers]
    B --> C[Services]
    C --> D[CharacterDbContext]
    D --> E[(SQL Server)]
```

## Request Flow

```mermaid
sequenceDiagram
    participant Client
    participant Controller
    participant Service
    participant DbContext
    participant SQL

    Client->>Controller: HTTP request
    Controller->>Service: Delegated operation
    Service->>DbContext: Query or command
    DbContext->>SQL: SQL execution
    SQL-->>DbContext: Data result
    DbContext-->>Service: Result
    Service-->>Controller: DTO / outcome
    Controller-->>Client: HTTP response
```

## Layer Responsibilities

| Layer                  | Responsibility                                             |
| ---------------------- | ---------------------------------------------------------- |
| **Controllers**        | Handle routes, model binding, and HTTP responses.          |
| **Services**           | Hold application logic and query behavior.                 |
| **DTOs**               | Define request and response contracts.                     |
| **CharacterDbContext** | Connect EF Core to SQL Server.                             |
| **Infrastructure**     | Support cross-cutting concerns such as exception handling. |

## Controller and Service Boundary

Controllers stay focused on HTTP concerns and delegate application work to services. Services contain the main read and write behavior of the API, including query shaping and entity-to-DTO mapping. This keeps endpoint actions thinner and makes behavior easier to locate and maintain.

## DTO Boundary

The API does not expose database entities directly. Request and response DTOs define the external contract, while entities remain part of the persistence model.

### Request DTOs

* `CreateCharacterRequest`
* `UpdateCharacterRequest`
* `LoginRequest`
* `GetCharactersQuery`

### Response DTOs

* `CharacterResponseDto`
* `PagedResponseDto<T>`
* `LoginResponse`

## Persistence Structure

Persistence is handled through Entity Framework Core and `CharacterDbContext`. The context translates LINQ queries into SQL, coordinates change tracking when needed, and works with migrations to keep the schema aligned with the code model.

## Composition Root

`Program.cs` acts as the composition root of the application. It wires together controllers, the database context, authentication and authorization, OpenAPI and Scalar, exception handling, and the HTTP request pipeline.
