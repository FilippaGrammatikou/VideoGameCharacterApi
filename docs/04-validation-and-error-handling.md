<h1 align="center">Validation and Error Handling</h1>

<p align="center"><em>Validation strategy and ProblemDetails behavior for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="ProblemDetails" src="https://img.shields.io/badge/ProblemDetails-081120?style=for-the-badge&logoColor=00E5FF" />
  <img alt="Validation" src="https://img.shields.io/badge/Validation-0B1020?style=for-the-badge&logoColor=FFD60A" />
</p>

---

## Overview

`VideoGameCharacterApi` validates input at the API boundary and returns structured error responses when requests are invalid or when unhandled failures occur.

The goal is to reject malformed input early, keep controller actions cleaner, and return more consistent responses to the client.

## Validation Layers

| Area                          | Purpose                                                             |
| ----------------------------- | ------------------------------------------------------------------- |
| **Request DTOs**              | Validate body input before application logic runs.                  |
| **Query Input**               | Constrain list and pagination-related request values.               |
| **Model Binding**             | Participate in request parsing and validation through ASP.NET Core. |
| **Global Exception Handling** | Convert unhandled failures into a consistent error response.        |

## Request Validation

Request DTOs use validation attributes so that invalid request bodies are rejected before the service layer processes them.

### Main Request DTOs

* `CreateCharacterRequest`
* `UpdateCharacterRequest`
* `LoginRequest`

These DTOs define the external contract of the API and act as the first validation boundary for incoming body data.

## Query Validation

List requests are also constrained through query-specific rules. This applies to pagination and other list-shaping parameters so that obviously invalid combinations do not flow deeper into the application.

In practical terms, query validation helps keep the list endpoint predictable and prevents malformed request values from being treated as normal application state.

## Role Validation

Character requests validate the `Role` field against the supported application-defined categories.

### Supported Role Categories

* `Protagonist`
* `Hero`
* `Antagonist`
* `Villain`

Values outside the supported set are rejected with a client error response.

## Validation Failure Behavior

When request validation fails, the API returns a client error response instead of continuing into service execution.

### Typical Validation Failure Cases

| Case                            | Result           |
| ------------------------------- | ---------------- |
| Missing required request fields | Request rejected |
| Invalid `Role` value            | Request rejected |
| Malformed query parameters      | Request rejected |
| Unsupported request shape       | Request rejected |

This prevents invalid data from reaching the persistence layer and makes failure behavior clearer to the client.

## ProblemDetails Behavior

The project is configured around a ProblemDetails-style error approach so that error responses are more structured than plain ad hoc strings.

This is especially useful for:

* validation failures
* request-shape issues
* unexpected runtime failures

A structured error format makes the API easier to inspect in Scalar or another client because failure responses are more readable and more consistent.

## Global Exception Handling

Unhandled exceptions are passed through a centralized exception handler rather than being handled separately inside multiple controller actions.

This keeps error behavior more uniform and reduces the amount of repetitive failure code that would otherwise appear across endpoints.
