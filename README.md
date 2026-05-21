# Example Project - ASP.NET Core Multi-Layered Architecture

This project is a highly scalable and maintainable **ASP.NET Core 10.0 Web API** template developed using modern software principles (SOLID, Clean Code) and best practices.

## 🚀 Key Features

- **Multi-Layered Architecture (N-Tier):** Clear separation of concerns with Entities, Dal, Business, Api, and Common layers.
- **Automatic Dependency Injection (DI):** Automatic service registration using Assembly Scanning with [Autofac](https://autofac.org/).
- **AOP (Aspect Oriented Programming):** Centralized management of **Auth**, **Log**, **Cache**, and **Validation** at the method level using [Castle DynamicProxy](http://www.castleproject.org/projects/dynamicproxy/).
- **Dynamic Authorization:** Request-time database-driven permission checks with short-term caching (MemoryCache), independent of token claims.
- **Automatic Permission Seeding (`ClaimSeedService`):** Automatically identifies and saves `[Auth]` attributes from the code into the database.
- **JWT Authentication:** Secure and standard-compliant JSON Web Token infrastructure.
- **Docker Support:** One-command setup for the entire environment (API + SQL Server).

## 🏗️ Architectural Structure

| Layer | Responsibility |
| :--- | :--- |
| **Example.Api** | Entry point of the application, Controllers, and Middleware configurations. |
| **Example.Business** | Business Logic, validations, and service implementations. |
| **Example.Dal** | Data Access Layer, EF Core Repository implementations, and DbContext. |
| **Example.Entities** | Database entities and Data Transfer Objects (DTOs). |
| **Example.Common** | Cross-cutting concerns, helper classes, and result models. |
| **Example.Core** | Centralized configurations and Autofac modules. |

## 🛠️ Technology Stack

- **Backend:** .NET 10.0
- **ORM:** Entity Framework Core 10.0
- **Database:** Microsoft SQL Server
- **DI/AOP:** Autofac & Castle DynamicProxy
- **Mapping:** AutoMapper
- **Documentation:** Swagger (Swashbuckle)

## 🚦 Quick Start

### Running with Docker
You can start the entire system (API + Database) by running the following command in the project root directory:

```bash
docker-compose up --build
```

When the application starts:
1. The database is automatically created.
2. An admin account is created with `admin@example.com` / `19Mayis1919!`.
3. All `[Auth]` attributes are scanned to populate the permission table.

**Swagger:** `http://localhost:5000`

## 🔐 Usage of Authorization

To add authorization control to a method, simply add the `[Auth]` attribute:

```csharp
[Auth("Product.Add")]
public async Task<IResult> AddProduct(ProductModel product)
{
    // ...
}
```

*Note: Permissions are checked against the database on every request, but are cached for 1 minute for performance.*

## 📄 License
This project is licensed under the [MIT](LICENSE) License.
