# Project Instructions - Example

This document provides foundational guidance for the **Example** project. Adhere to these standards to maintain consistency and architectural integrity.

## Project Overview
A multi-layered ASP.NET Core Web API project using Autofac for Dependency Injection and Castle DynamicProxy for Aspect-Oriented Programming (AOP).

## Architecture & Layers
The project follows an N-Tier Architecture:

- **Example.Api:** The entry point. Modern unified `Program.cs` structure.
- **Example.Business:** Business logic layer.
- **Example.Dal:** Data Access Layer.
- **Example.Entities:** Domain entities and Data Transfer Objects (DTOs).
- **Example.Common:** Shared cross-cutting concerns.
- **Example.Core:** Dependency Injection configuration (Autofac).

## Technology Stack
- **Framework:** .NET 10.0
- **DI Container:** Autofac
- **AOP:** Castle DynamicProxy
- **ORM:** Entity Framework Core 10.0
- **Auth:** JWT (JSON Web Tokens)
- **Documentation:** Swagger/OpenAPI (Swashbuckle)

## Coding Conventions

### Language Features
- **C# 14:** Utilize modern C# features such as File-scoped namespaces, Global Usings, and Primary Constructors where appropriate.
- **Minimal API / Unified Program.cs:** The Api layer uses the unified `Program.cs` pattern.

### Result Pattern
All Business layer methods MUST return an `IResult` or `IDataResult<T>`.

### Aspect-Oriented Programming (AOP)
Cross-cutting concerns are handled via attributes on Manager methods.

### Mapping
Automated mapping is handled via **AutoMapper**.

### Async/Await
Use asynchronous programming throughout the layers.

## Development Workflow
(Steps remain the same, ensuring net10.0 compatibility)
