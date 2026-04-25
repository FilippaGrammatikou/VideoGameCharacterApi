<h1 align="center">Authentication and Authorization</h1>

<p align="center"><em>JWT setup and authorization model for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="JWT" src="https://img.shields.io/badge/JWT-081120?style=for-the-badge&logo=jsonwebtokens&logoColor=FF2DAA" />
  <img alt="Authorization" src="https://img.shields.io/badge/Authorization-0B1020?style=for-the-badge&logoColor=FFD60A" />
</p>

---

## Overview

`VideoGameCharacterApi` uses JWT bearer authentication and policy-based authorization. Login returns a signed token, and protected endpoints require that token in the `Authorization` header.

## Login Endpoint

| Item             | Value                  |
| ---------------- | ---------------------- |
| **Route**        | `POST /api/Auth/login` |
| **Access**       | Public                 |
| **Request DTO**  | `LoginRequest`         |
| **Response DTO** | `LoginResponse`        |

### Request Body

```json
{
  "username": "admin",
  "password": "admin123"
}
```

### Response Shape

```json
{
  "token": "<jwt>",
  "role": "Admin"
}
```

## Demo Accounts

| Username | Password   | Role    |
| -------- | ---------- | ------- |
| `user`   | `user123`  | `User`  |
| `admin`  | `admin123` | `Admin` |

Invalid credentials return `401 Unauthorized`.

## Bearer Token Usage

After login, the returned token must be sent in the request header:

```http
Authorization: Bearer <token>
```

Requests to protected endpoints without a valid token return `401 Unauthorized`.

## Token Contents

The token is created with two application-relevant claims:

* `Name`
* `Role`

It is signed with the configured JWT key and issued with a one-hour expiration window.

## Validation Settings

The API validates incoming tokens against the configured:

* issuer
* audience
* signing key
* expiration time

This is configured in `Program.cs` through ASP.NET Core JWT bearer authentication.

## Authorization Policies

The project currently defines two authorization policies.

| Policy        | Allowed Roles   |
| ------------- | --------------- |
| `UserOrAdmin` | `User`, `Admin` |
| `AdminOnly`   | `Admin`         |

## Endpoint Access Model

| Endpoint                               | Policy        |
| -------------------------------------- | ------------- |
| `GET /api/VideoGameCharacters`         | `UserOrAdmin` |
| `GET /api/VideoGameCharacters/{id}`    | `UserOrAdmin` |
| `POST /api/VideoGameCharacters`        | `AdminOnly`   |
| `PUT /api/VideoGameCharacters/{id}`    | `AdminOnly`   |
| `DELETE /api/VideoGameCharacters/{id}` | `AdminOnly`   |

Requests made with a valid token but insufficient privileges return `403 Forbidden`.
