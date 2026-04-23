<h1 align="center">API Reference</h1>

<p align="center"><em>Endpoint-by-endpoint reference for VideoGameCharacterApi.</em></p>

<p align="center">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-0B1020?style=for-the-badge&logo=dotnet&logoColor=FF2DAA" />
  <img alt="JWT" src="https://img.shields.io/badge/JWT-081120?style=for-the-badge&logo=jsonwebtokens&logoColor=FF2DAA" />
  <img alt="Scalar" src="https://img.shields.io/badge/Scalar-0B1020?style=for-the-badge&logoColor=00E5FF" />
</p>

---

## Base Information

| Item                 | Value                                   |
| -------------------- | --------------------------------------- |
| **Base URL**         | `http://localhost:8080`                 |
| **Documentation UI** | `http://localhost:8080/scalar`          |
| **OpenAPI JSON**     | `http://localhost:8080/openapi/v1.json` |

## Authentication Summary

Authentication uses JWT bearer tokens.

1. Call `POST /api/Auth/login`
2. Copy the returned token
3. Send it in the request header:

```http
Authorization: Bearer <token>
```

## Endpoint Index

| Method   | Route                           | Access        |
| -------- | ------------------------------- | ------------- |
| `GET`    | `/`                             | Public        |
| `POST`   | `/api/Auth/login`               | Public        |
| `GET`    | `/api/VideoGameCharacters`      | User or Admin |
| `GET`    | `/api/VideoGameCharacters/{id}` | User or Admin |
| `POST`   | `/api/VideoGameCharacters`      | Admin         |
| `PUT`    | `/api/VideoGameCharacters/{id}` | Admin         |
| `DELETE` | `/api/VideoGameCharacters/{id}` | Admin         |

---

## `GET /`

Returns the root route response of the application. In the current setup, this route is used as a convenience entry point for the API UI rather than as a business endpoint.

| Item              | Value                                                        |
| ----------------- | ------------------------------------------------------------ |
| **Access**        | Public                                                       |
| **Purpose**       | Root application route                                       |
| **Response Type** | HTML or redirect behavior, depending on current root mapping |

---

## `POST /api/Auth/login`

Authenticates a user and returns a JWT token.

| Item             | Value               |
| ---------------- | ------------------- |
| **Access**       | Public              |
| **Purpose**      | Obtain bearer token |
| **Request DTO**  | `LoginRequest`      |
| **Response DTO** | `LoginResponse`     |

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

### Demo Accounts

| Username | Password   | Role    |
| -------- | ---------- | ------- |
| `user`   | `user123`  | `User`  |
| `admin`  | `admin123` | `Admin` |

---

## `GET /api/VideoGameCharacters`

Returns a paginated list of characters.

| Item             | Value                                    |
| ---------------- | ---------------------------------------- |
| **Access**       | User or Admin                            |
| **Purpose**      | List characters                          |
| **Query DTO**    | `GetCharactersQuery`                     |
| **Response DTO** | `PagedResponseDto<CharacterResponseDto>` |

### Supported Query Parameters

| Parameter       | Type    | Notes                |
| --------------- | ------- | -------------------- |
| `page`          | integer | Page number          |
| `pageSize`      | integer | Page size            |
| `game`          | string  | Optional filter      |
| `role`          | string  | Optional filter      |
| `sortBy`        | string  | Supported sort field |
| `sortDirection` | string  | Sort direction       |

### Example Request

```http
GET /api/VideoGameCharacters?page=1&pageSize=10&game=Tekken&sortBy=Name&sortDirection=asc
Authorization: Bearer <token>
```

### Response Shape

```json
{
  "page": 1,
  "pageSize": 10,
  "totalCount": 2,
  "items": [
    {
      "id": 1,
      "name": "Jin Kazama",
      "game": "Tekken",
      "role": "Protagonist"
    }
  ]
}
```

---

## `GET /api/VideoGameCharacters/{id}`

Returns a single character by identifier.

| Item                | Value                  |
| ------------------- | ---------------------- |
| **Access**          | User or Admin          |
| **Purpose**         | Retrieve one character |
| **Route Parameter** | `id`                   |
| **Response DTO**    | `CharacterResponseDto` |

### Example Request

```http
GET /api/VideoGameCharacters/1
Authorization: Bearer <token>
```

### Response Shape

```json
{
  "id": 1,
  "name": "Jin Kazama",
  "game": "Tekken",
  "role": "Protagonist"
}
```

---

## `POST /api/VideoGameCharacters`

Creates a new character.

| Item             | Value                    |
| ---------------- | ------------------------ |
| **Access**       | Admin                    |
| **Purpose**      | Create character         |
| **Request DTO**  | `CreateCharacterRequest` |
| **Response DTO** | `CharacterResponseDto`   |

### Request Body

```json
{
  "name": "Kazuya Mishima",
  "game": "Tekken",
  "role": "Antagonist"
}
```

### Response Shape

```json
{
  "id": 5,
  "name": "Kazuya Mishima",
  "game": "Tekken",
  "role": "Antagonist"
}
```

### Role Rules

The `role` field is restricted to the supported role categories used by the API:

* `Protagonist`
* `Hero`
* `Antagonist`
* `Villain`

Invalid values are rejected by validation.

---

## `PUT /api/VideoGameCharacters/{id}`

Updates an existing character.

| Item                | Value                    |
| ------------------- | ------------------------ |
| **Access**          | Admin                    |
| **Purpose**         | Update character         |
| **Route Parameter** | `id`                     |
| **Request DTO**     | `UpdateCharacterRequest` |
| **Response DTO**    | `CharacterResponseDto`   |

### Request Body

```json
{
  "name": "Kazuya Mishima",
  "game": "Tekken 8",
  "role": "Villain"
}
```

### Response Shape

```json
{
  "id": 5,
  "name": "Kazuya Mishima",
  "game": "Tekken 8",
  "role": "Villain"
}
```

---

## `DELETE /api/VideoGameCharacters/{id}`

Deletes a character by identifier.

| Item                | Value                 |
| ------------------- | --------------------- |
| **Access**          | Admin                 |
| **Purpose**         | Delete character      |
| **Route Parameter** | `id`                  |
| **Response**        | No content on success |

### Example Request

```http
DELETE /api/VideoGameCharacters/5
Authorization: Bearer <token>
```

### Success Result

```http
204 No Content
```

---

## Common Responses

| Status Code                 | Meaning                             |
| --------------------------- | ----------------------------------- |
| `200 OK`                    | Successful read or login            |
| `201 Created`               | Successful create                   |
| `204 No Content`            | Successful delete                   |
| `400 Bad Request`           | Validation or request-shape failure |
| `401 Unauthorized`          | Missing or invalid bearer token     |
| `403 Forbidden`             | Authenticated but insufficient role |
| `404 Not Found`             | Requested resource does not exist   |
| `500 Internal Server Error` | Unhandled server-side failure       |

## Models Used by the API

* `LoginRequest`
* `LoginResponse`
* `CreateCharacterRequest`
* `UpdateCharacterRequest`
* `GetCharactersQuery`
* `CharacterResponseDto`
* `PagedResponseDto<T>`
