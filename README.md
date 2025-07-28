# Blog Web App

A full-stack blogging web application built with **Clean Architecture** on the backend (ASP.NET Core, EF Core) and a modern React + Vite + Tailwind CSS frontend.  
This README is written for clear setup steps, architectural highlights, and a roadmap for next steps.

---
## 🔧 Prerequisites

- **.NET 9.0 SDK**  
- **SQL Server** (LocalDB or full)  
- **Node.js** ≥ 16 & **npm** (or Yarn)  
- (Windows) Trust the HTTPS dev cert:  
  ```bash
  dotnet dev-certs https --trust

## Environment Setup & Run
- Backend
  
Open a terminal and navigate to the API project:

```bash
cd blog-backend/src/Blog.Api

Restore NuGet packages:

```bash
dotnet restore
Install dotnet ef tool globally:
```bash 
dotnet tool install --global dotnet-ef

Apply EF Core migrations & create database:

```bash
dotnet ef database update

Run the API (will listen on HTTP & HTTPS as per launchSettings.json):

```bash
dotnet run



  Verify:

        Swagger UI → https://localhost:7107/swagger/index.html


Frontend

  Open a new terminal at the React app root:

```bash
cd blog-frontend

Install dependencies:

```bash
npm install

Copy env example & configure:

```bash
cp .env.example .env

# Edit .env → e.g. VITE_API_BASE_URL=https://localhost:7107/api

Start the dev server:

```bash
npm run dev

Visit: http://localhost:5173

## Project Structure & Architectural Highlights
/blog-backend
 ├─ src
 │   ├─ Blog.Api              ← ASP.NET Core Web API
 │   ├─ Blog.Application      ← DTOs, interfaces, services, validators
 │   ├─ Blog.Domain           ← Entities, core business rules
 │   └─ Blog.Persistence      ← EF Core DbContext, repositories
 └─ tests                    ← Unit & integration tests (xUnit)

/blog-frontend
 ├─ src
 │   ├─ api                  ← axios wrappers (blogPostsApi, commentsApi)
 │   ├─ components           ← cards, skeletons, layout, ErrorBoundary
 │   ├─ pages                ← list, detail, create pages
 │   └─ lib                  ← axiosInstance, config

## Clean Architecture:

- Domain: core entities & validation
- Application: service interfaces, DTOs, FluentValidation, AutoMapper
- Persistence: EF Core, repository pattern
- API: controllers, middleware (global exception handling, CORS policy)
- Async, loosely-coupled services & repositories 
- DTOs for request/response shape, AutoMapper for mapping
- Global Middleware for consistent error responses & logging
- Swagger for self-documenting endpoints
- Tailwind CSS for utility-first styling, Vite for fast HMR

## Tech Stack & Tools
| Layer         | Technology                              | Version |
| ------------- | --------------------------------------- | ------- |
| Backend       | .NET 9.0, ASP.NET Core Web API          | 9.0.x   |
| Persistence   | EF Core 9.0, SQL Server LocalDB/Express | 9.0.x   |
| Validation    | FluentValidation                        | 11.x    |
| Mapping       | AutoMapper                              | 12.x    |
| Testing       | xUnit, FluentValidation.TestHelper      | Latest  |
| Frontend      | React 18, Vite, Tailwind CSS 3.x        |         |
| HTTP Client   | Axios                                   | Latest  |
| Routing       | React Router v6                         |         |
| Notifications | React-Toastify                          | Latest  |

✨ Thank you for reviewing! ✨
This repo demonstrates a production-ready Clean-Architecture solution, asynchronous REST APIs, and a modern React front end—all designed for maintainability, and ease of review.
