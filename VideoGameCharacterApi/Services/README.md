<div align="center">

# Services

### Service abstraction and character-related application logic

![Services](https://img.shields.io/badge/Services-A7D8FF?style=for-the-badge&logoColor=000000)
![Layered%20Design](https://img.shields.io/badge/Layered%20Design-A7D8FF?style=for-the-badge&logoColor=000000)

</div>

---

## Overview

This folder contains the service layer used by the API.

Services sit between the controllers and the database context, helping keep the endpoint layer focused on HTTP concerns while moving character operations into a separate part of the application.

---

## Contents

- `IVideoGameCharacterService`
- `VideoGameService`

---

## Purpose

This folder supports a cleaner separation of responsibilities by keeping CRUD logic outside the controller layer.
