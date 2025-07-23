# 🎬 Cinema Ticket Booking System

A full-featured web-based cinema ticket booking system that allows users to explore movies, schedules, and book tickets, while providing admins with powerful tools to manage content such as movies, genres, actors, showtimes, and more — all built using **Clean Architecture** and **N-Layer Architecture** principles.

---

## 💡 Project Overview

The **Cinema Ticket Booking System** is built using **N-Layered Architecture**, applying the **Clean Architecture** approach to ensure separation of concerns, scalability, and maintainability.

The system is divided into well-defined layers:
- **Presentation Layer** (API)
- **Application Layer** (DTOs, Interfaces, Services)
- **Domain Layer** (Business Entities, Enums)
- **Infrastructure Layer** (Database access, Third-party integrations like Payments and Caching)
- **Data Layer** (Metadata, Constants, Localization)

---

## ✅ Key Features

- 🧩 N-Layer Architecture (API, Application, Domain, Infrastructure, Data)
- 🧼 Clean Architecture structure and principles
- 🎞️ Full CRUD for movies, genres, and actors
- 👥 Manage actor-movie many-to-many relationships
- 🕒 Schedule movie showtimes in different cinemas
- 🎟️ Ticket booking system with dynamic data linking user, movie, and schedule
- 💳 Integrated with payment gateway (Stripe or similar)
- 💾 In-memory caching for better performance
- 🛡️ Custom middleware for global exception handling
- 🧪 Global action filters for unified validation and error response
- 📦 Serilog for structured error logging
- 🌐 Swagger/OpenAPI documentation
- 🗂️ Arabic localization support using `.resx` resource files

---

## 🛠️ Technologies Used

- **ASP.NET Core 8 Web API**
- **Entity Framework Core (Code First)**
- **SQL Server**
- **AutoMapper**
- **Repository & Unit of Work Patterns**
- **Clean Architecture + N-Layer Structure**
- **Serilog** for logging errors and requests
- **Custom Middleware** for centralized exception handling
- **Global Filters** for consistent API behavior
- **In-Memory Caching**
- **Payment Gateway Integration (e.g., Stripe)**
- **Swagger / Postman** for API testing & docs
- **Git & GitHub** for source control

---



