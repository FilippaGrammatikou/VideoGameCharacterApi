<h1 align="center">Project Overview</h1>

<p align="center"><em>Scope, goals, and project rationale for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="EF Core" src="https://img.shields.io/badge/EF%20Core-081120?style=for-the-badge&logoColor=00E5FF" />
  <img alt="SQL Server" src="https://img.shields.io/badge/SQL%20Server-0B1020?style=for-the-badge&logo=microsoftsqlserver&logoColor=FFD60A" />
</p>

---

## Scope

`VideoGameCharacterApi` is a backend-focused ASP.NET Core Web API project built to consolidate practical server-side development knowledge inside a single, structured application.

The scope is deliberately broader than a minimal CRUD exercise. The project combines API layering, DTO-based contracts, Entity Framework Core persistence, SQL Server integration, authentication, authorization, validation, testing, and Dockerized local delivery in one codebase so that each part is understood in relation to the others.

## Primary Goals

| Goal                             | Meaning in This Project                                                                                                                         |
| -------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------- |
| **Build Backend Fluency**        | Strengthen practical ASP.NET Core Web API development through implementation rather than isolated theory.                                       |
| **Work with Real Structure**     | Organize the application through controllers, services, DTOs, infrastructure components, and database access layers.                            |
| **Model Real API Concerns**      | Include authentication, authorization, validation, query shaping, and error handling instead of limiting the project to simple endpoint wiring. |
| **Practice Persistence**         | Use Entity Framework Core and SQL Server in a way that connects application logic to real database behavior.                                    |
| **Support Repeatable Execution** | Run the project through Docker so the local delivery flow is easier to reproduce and evaluate.                                                  |

## What the Project Tries to Demonstrate

The project is intended to demonstrate disciplined backend construction rather than novelty. Its emphasis is on clarity of structure, consistency of API contracts, and better separation between HTTP-facing behavior, application logic, and persistence.

It also serves as a practice ground for understanding how separate concerns fit together in a backend application: how DTOs protect the API contract, how services shape behavior, how validation limits invalid input, how authentication and authorization affect endpoint access, how tests verify behavior, and how containerization changes the local execution story.

## Project Boundaries

This project is not intended to function as a production-ready enterprise system. It does not attempt to cover every possible backend concern, nor does it present itself as a finished commercial platform.

Its purpose is narrower and more useful: to act as a technically serious learning project that is structured clearly enough to study, test, run, and extend.

## Why This Project Exists

The project exists to move beyond fragmented tutorial knowledge and toward a more connected understanding of backend engineering. Instead of treating persistence, authentication, validation, testing, and delivery as unrelated topics, it places them inside one application so that the relationships between them become clearer in practice.

That makes the repository useful not only as code, but also as a record of how a more complete ASP.NET Core API can be assembled step by step.

