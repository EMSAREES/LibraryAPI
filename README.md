#  LibraryAPI

API REST para la gestión de una biblioteca, desarrollada con **.NET** utilizando **Clean Architecture + CQRS + DDD**.

---

##  Descripción

**LibraryAPI** es un sistema backend que permite gestionar:

* Préstamos de libros
* Disponibilidad y stock
* Reservas
* Multas y restricciones
* Usuarios con roles (Administrador, Supervisor, Empleado)
* Auditoría de acciones

El proyecto está diseñado siguiendo buenas prácticas de arquitectura para ser **escalable, mantenible y profesional**.

---

##  Arquitectura

El proyecto utiliza:

* ✅ **Clean Architecture**
* ✅ **CQRS (Command Query Responsibility Segregation)**
* ✅ **DDD (Domain-Driven Design)**
* ✅ **Repository Pattern + Unit of Work**

###  Estructura del proyecto

```
LibraryAPI/
│
├── src/
│   ├── LibraryAPI.Domain/          → Reglas de negocio
│   ├── LibraryAPI.Application/     → Casos de uso (CQRS)
│   ├── LibraryAPI.Infrastructure/  → Base de datos, servicios externos
│   └── LibraryAPI.API/             → Controllers y configuración
│
└── tests/
    ├── LibraryAPI.UnitTests/
    └── LibraryAPI.IntegrationTests/
```

---

##  Dominio del negocio

El sistema implementa reglas como:

* Un usuario no puede tener más de **N préstamos activos**
* No puede pedir libros si tiene **multas pendientes**
* Un libro debe estar **disponible** para ser prestado
* Se generan **multas por retraso**
* Se pueden hacer **reservas**
* Se registra **auditoría de acciones**

---

##  Flujo de una operación (Ejemplo: préstamo)

1. `POST /api/loans`
2. Controller envía un **Command**
3. MediatR procesa el comando
4. Se ejecutan:

   * Validaciones
   * Lógica de negocio (Domain)
5. Se guarda en base de datos (EF Core)
6. Se registra auditoría
7. Se devuelve respuesta

---

##  Tecnologías utilizadas

* **.NET / ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **MediatR** (CQRS)
* **FluentValidation**
* **JWT Authentication**
* **Mapster / AutoMapper**
* **xUnit** (Testing)

---

##  Configuración

###  Variables importantes (`appsettings.json`)

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=librarydb;Username=postgres;Password=1234"
},
"Jwt": {
  "Key": "SUPER_SECRET_KEY",
  "Issuer": "LibraryAPI",
  "Audience": "LibraryUsers"
}
```

---

##  Ejecutar el proyecto

```bash
dotnet restore
dotnet build
dotnet run --project src/LibraryAPI.API
```

---

##  Migraciones (EF Core)

```bash
dotnet ef migrations add InitialCreate --project src/LibraryAPI.Infrastructure --startup-project src/LibraryAPI.API

dotnet ef database update --project src/LibraryAPI.Infrastructure --startup-project src/LibraryAPI.API
```

---

##  Autenticación

El sistema utiliza **JWT Bearer Token** para autenticación y control de acceso por roles.

---

##  Testing

* Unit Tests → lógica de dominio y aplicación
* Integration Tests → endpoints y base de datos

---

##  Estado del proyecto

🚧 En desarrollo — enfocado en aprendizaje y buenas prácticas profesionales.

---

##  Objetivo del proyecto

Este proyecto fue creado para:

* Aprender arquitectura profesional en .NET
* Implementar DDD en un caso real
* Prepararse para proyectos reales o trabajo en equipo

---

##  Contribuciones

Este proyecto es de aprendizaje, pero puedes:

* Hacer fork
* Probar mejoras
* Practicar arquitectura

---

##  Licencia

Uso libre para fines educativos.


##  Orden de cada archivo o como lo hice yo porque ando aprendiendo
Domain/
├── Enums/          
├── Common/         ← primero esto
├── Exceptions/     ← luego esto
├── ValueObjects/   ← luego esto
└── Entities/       ← al final esto