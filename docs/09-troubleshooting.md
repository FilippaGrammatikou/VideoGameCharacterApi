<h1 align="center">Troubleshooting</h1>

<p align="center"><em>Common local setup and runtime issues for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="SQL Server" src="https://img.shields.io/badge/SQL%20Server-081120?style=for-the-badge&logo=microsoftsqlserver&logoColor=FFD60A" />
  <img alt="Docker" src="https://img.shields.io/badge/Docker-0B1020?style=for-the-badge&logo=docker&logoColor=00E5FF" />
</p>

---

## Overview

This document collects common setup, execution, and testing issues that may appear while running `VideoGameCharacterApi` locally or through Docker.

## Common Issues

### API root returns `404 Not Found`

The application is an API project, not a traditional website. A `404` at the base URL does not necessarily mean the application failed to start.

Use one of the intended routes instead:

* `http://localhost:8080/scalar`
* `http://localhost:8080/openapi/v1.json`
* `http://localhost:8080/api/...`

### `401 Unauthorized`

This usually means the request was sent without a valid bearer token.

Check that:

* login was performed through `POST /api/Auth/login`
* the returned JWT is attached as `Authorization: Bearer <token>`
* the token is not expired or malformed

### `403 Forbidden`

This means authentication succeeded, but the caller does not have the required role for the endpoint.

Use an account with the correct permissions for endpoints restricted to administrative access.

### Validation failures on create or update requests

If a create or update request returns a client error response, verify that:

* required fields are present
* request JSON matches the expected DTO shape
* `Role` uses one of the supported application-defined categories

### Pagination or query parameter issues

If the list endpoint rejects a request, review the supplied query values for malformed or unsupported input.

This usually affects:

* `page`
* `pageSize`
* `sortBy`
* `sortDirection`
* optional filter values

### Database connection problems in local runs

If the API cannot connect to SQL Server locally, verify:

* the SQL Server instance is running
* the connection string in `appsettings.json` is correct
* the target database is accessible
* migrations have been applied if required

### EF Core migration command issues

Migration commands can fail if they are executed from the wrong location or if the project context is unclear.

Typical checks:

* run commands from the correct project root
* confirm the startup project is the API project
* confirm EF Core tools are installed and available

### Docker command not recognized

If `docker` is not recognized in the terminal, Docker Desktop is likely not installed correctly, not running, or not available in the current shell session.

Check that:

* Docker Desktop is installed
* Docker Desktop is running
* the terminal was reopened after installation
* `docker --version` works

### Docker Compose cannot find files

If Docker reports missing files such as restore scripts or database backups, confirm that:

* the file exists at the expected path
* the filename is correct
* file extensions are not hidden incorrectly by Windows
* the path in `docker-compose.yml` matches the real folder structure

### Restore script errors in Docker

If the database restore step fails, verify:

* the backup file exists in the expected `database/` folder
* the restore script filename is correct
* the script uses Linux line endings (`LF`), not Windows line endings (`CRLF`)
* the SQL Server container version is compatible with the backup source version

### SQL Server version mismatch during restore

A backup created on a newer SQL Server version cannot be restored to an older SQL Server container.

If restore fails with a version-compatibility message, the SQL Server container image must be aligned with the source engine version of the backup.

### Scalar opens but endpoints do not behave as expected

If Scalar loads but requests fail, check:

* the API container is actually running
* the database is available
* the correct token is attached for protected endpoints
* the request body matches the expected DTO shape

### Tests fail unexpectedly

If tests fail after application changes, review whether the change altered:

* endpoint authorization behavior
* validation rules
* query logic
* startup configuration used by the integration tests

Also confirm that `CustomWebApplicationFactory` still boots the API correctly for the test suite.

## Quick Checks

| Check                          | Purpose                                        |
| ------------------------------ | ---------------------------------------------- |
| `dotnet test`                  | Verify the test suite still passes             |
| `docker --version`             | Verify Docker is installed and available       |
| `docker compose up --build`    | Verify the containerized flow starts correctly |
| `http://localhost:8080/scalar` | Verify the API UI is reachable                 |
| `POST /api/Auth/login`         | Verify authentication is functioning           |

