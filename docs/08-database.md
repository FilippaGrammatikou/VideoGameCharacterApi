<h1 align="center">Database</h1>

<p align="center"><em>EF Core, SQL Server, and migration notes for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="Entity Framework Core" src="https://img.shields.io/badge/EF%20Core-0B1020?style=for-the-badge&logoColor=00E5FF" />
  <img alt="SQL Server" src="https://img.shields.io/badge/SQL%20Server-081120?style=for-the-badge&logo=microsoftsqlserver&logoColor=FFD60A" />
  <img alt="Migrations" src="https://img.shields.io/badge/Migrations-0B1020?style=for-the-badge&logoColor=FF2DAA" />
</p>

---

## Overview

`VideoGameCharacterApi` uses SQL Server for persistence and Entity Framework Core as its data access layer. Database interaction is centered on `CharacterDbContext`, while schema changes are tracked through EF Core migrations.

## Persistence Stack

| Item                 | Role                          |
| -------------------- | ----------------------------- |
| `CharacterDbContext` | Main EF Core database context |
| SQL Server           | Persistence engine            |
| EF Core Migrations   | Schema evolution tracking     |

## `CharacterDbContext`

`CharacterDbContext` acts as the bridge between the application model and SQL Server. It exposes the entity set used by the API and allows EF Core to translate LINQ queries and updates into SQL operations.

## Entity Model

The database-backed entity used by the project is the character model stored in the application’s persistence layer.

### Main Stored Fields

| Field  | Purpose                 |
| ------ | ----------------------- |
| `Id`   | Primary identifier      |
| `Name` | Character name          |
| `Game` | Associated game title   |
| `Role` | Character role category |

The API returns these values through response DTOs rather than exposing persistence entities directly.

## Migrations

Schema changes are managed through the `Migrations/` folder. Each migration records a database change so that the schema can be recreated or updated in a controlled way.

### Current Migration Role

* initialize the schema
* track later schema changes
* keep database structure aligned with the code model

## Applying Migrations

For local development, migrations can be applied through EF Core tooling when needed.

```bash
dotnet ef database update
```

This updates the target database to the latest migration state.

## Docker Database Flow

In the Docker setup, SQL Server is started as a container and the database state is restored before the API container begins serving requests.

That means the containerized database flow is not limited to creating an empty schema. It is intended to run the project against the expected application database state.

## Query and Update Behavior

The service layer uses EF Core through `CharacterDbContext` for:

* character retrieval
* filtered and paginated list queries
* create operations
* update operations
* delete operations

This keeps database access inside the application layer rather than scattering it across controllers.
