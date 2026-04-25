<h1 align="center">Testing</h1>

<p align="center"><em>Testing structure and methodology for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="xUnit" src="https://img.shields.io/badge/xUnit-0B1020?style=for-the-badge&logoColor=FF2DAA" />
  <img alt="Integration Tests" src="https://img.shields.io/badge/Integration-081120?style=for-the-badge&logoColor=00E5FF" />
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FFD60A" />
</p>

---

## Overview

`VideoGameCharacterApi` uses automated tests to verify both isolated rules and HTTP-level behavior. The test suite is split between focused rule tests and integration tests that exercise the API through a hosted test application.

## Test Project

| Item           | Value                                                                         |
| -------------- | ----------------------------------------------------------------------------- |
| **Project**    | `VideoGameCharacterApi.Tests`                                                 |
| **Framework**  | xUnit                                                                         |
| **Main Focus** | Query rules, authentication, authorization, validation, and endpoint behavior |

## Test Structure

| Area                  | Purpose                                                           |
| --------------------- | ----------------------------------------------------------------- |
| **Rule Tests**        | Verify small application rules in isolation.                      |
| **Integration Tests** | Verify endpoint behavior through HTTP requests and responses.     |
| **Factory Setup**     | Provide a shared test host through `CustomWebApplicationFactory`. |

## Current Coverage

The current test suite covers:

* pagination and query-rule behavior
* authentication success behavior
* authorization boundaries
* validation failure behavior
* successful endpoint execution paths

## Integration Testing Approach

Integration tests use `CustomWebApplicationFactory` to boot the application and create an `HttpClient` against the hosted API.

This allows tests to verify behavior at the HTTP boundary, including:

* status codes
* authentication requirements
* authorization rules
* request and response handling

## `CustomWebApplicationFactory`

`CustomWebApplicationFactory` is kept intentionally minimal. Its purpose is to provide a shared ASP.NET Core test host for integration tests without duplicating bootstrapping logic across test classes.

## Example Test Categories

| Test Category              | Purpose                                                                 |
| -------------------------- | ----------------------------------------------------------------------- |
| **Authentication Tests**   | Verify login behavior and token-related success cases.                  |
| **Authorization Tests**    | Verify protected endpoints reject requests without the required access. |
| **Validation Tests**       | Verify invalid requests are rejected with client error responses.       |
| **Success Endpoint Tests** | Verify valid requests complete successfully.                            |
| **Query Rule Tests**       | Verify pagination-related rules outside the HTTP layer.                 |

## Run Tests

```bash
dotnet test
```

