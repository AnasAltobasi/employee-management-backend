# Employee Management System - Backend API

## 📌 Project Overview

This project is a RESTful Web API built using ASP.NET Core following Clean Architecture principles.

The system provides full CRUD (Create, Read, Update, Delete) functionality for managing employee records.

This backend is structured using a layered architecture to ensure separation of concerns, maintainability, scalability, and clean code practices.

## 🏗 Architecture

This solution follows Clean Architecture with the following layers:

- **Web (Presentation Layer)** → Handles HTTP requests and responses
- **Application Layer** → Business logic and use cases
- **Domain Layer** → Core entities and business rules
- **Infrastructure Layer** → Database access and external services
- **Shared Layer** → Common utilities and shared models

## 🛠 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / SQLite
- Swagger (OpenAPI)
- .NET 8
- Clean Architecture

## 📂 Solution Structure

EmployeeManagementSystem/

EmployeeManagementSystem.Web → Web API (Presentation Layer)
EmployeeManagementSystem.Application → Application Layer
EmployeeManagementSystem.Domain → Domain Layer
EmployeeManagementSystem.Infrastructure → Infrastructure Layer
EmployeeManagementSystem.Shared → Shared/Common Layer
EmployeeManagementSystem.sln

