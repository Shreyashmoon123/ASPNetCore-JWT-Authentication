# 🔐 ASP.NET Core JWT Authentication API

A secure and scalable **ASP.NET Core Web API** implementing **JWT Authentication, ASP.NET Core Identity, and Role-Based Authorization**.

This project demonstrates how user registration, login, password verification, JWT token generation, authentication, and authorization can be implemented in a modern ASP.NET Core Web API.

## 🚀 Features

* 🔐 User Registration
* 🔑 User Login
* 🎟️ JWT Token Generation
* 🛡️ JWT Authentication
* 👤 ASP.NET Core Identity
* 👥 Role-Based Authorization
* 📋 Claims-Based Information inside JWT
* 🔒 Protected API Endpoints
* 🗄️ SQL Server Database
* 🧩 Entity Framework Core
* 📚 Swagger / OpenAPI API Documentation
* 🔑 Secure Password Hashing using ASP.NET Core Identity

## 🛠️ Technologies Used

| Technology            | Purpose                     |
| --------------------- | --------------------------- |
| C#                    | Programming Language        |
| ASP.NET Core Web API  | Backend API                 |
| ASP.NET Core Identity | User & Role Management      |
| JWT                   | Authentication              |
| Entity Framework Core | ORM / Database Access       |
| SQL Server            | Database                    |
| Swagger / OpenAPI     | API Testing & Documentation |
| .NET                  | Application Framework       |

## 🔄 Authentication Flow

```text
User
 │
 ├── Register
 │      ↓
 │   ASP.NET Core Identity
 │      ↓
 │   SQL Server
 │
 └── Login
        ↓
   Verify Email & Password
        ↓
   Generate JWT Token
        ↓
   Return Token
        ↓
   Client Sends Bearer Token
        ↓
   JWT Authentication
        ↓
   Protected API
```

## 📌 API Endpoints

### Authentication

#### Register

```http
POST /api/UserAuth/Register
```

Creates a new user using ASP.NET Core Identity.

#### Login

```http
POST /api/UserAuth/Login
```

Validates user credentials and returns a JWT token.

Example response:

```json
{
  "success": true,
  "token": "your-jwt-token"
}
```

### Protected API

Protected endpoints require a valid JWT Bearer token.

```http
Authorization: Bearer <your-jwt-token>
```

## 🔐 JWT Authentication

After successful login, the API generates a JWT containing user information such as:

* User ID
* Email
* Name
* JWT ID

The generated token is then used to access protected API endpoints.

## 👥 Role-Based Authorization

The project also demonstrates **Role-Based Authorization**, allowing API access to be restricted according to the user's assigned role.

Example:

```csharp
[Authorize(Roles = "Admin")]
```

This ensures that only authenticated users with the required role can access specific endpoints.

## 🗄️ Database

The application uses:

* **SQL Server**
* **Entity Framework Core**
* **ASP.NET Core Identity**

Identity manages users, passwords, roles, and related authentication data.

## 🧪 Testing with Swagger

Swagger / OpenAPI is configured for API testing.

Basic workflow:

```text
1. Register a user
2. Login with registered credentials
3. Copy the generated JWT token
4. Authorize using Bearer Token
5. Access protected endpoints
6. Test role-based authorization
```

## ⚙️ Configuration

Before running the project, configure your database connection and JWT settings in `appsettings.json`.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY",
    "Issuer": "YOUR_ISSUER",
    "Audience": "YOUR_AUDIENCE",
    "ExpiryMinutes": "60"
  }
}
```

> ⚠️ Never commit real database passwords, JWT secret keys, API keys, or other sensitive credentials to GitHub.

## ▶️ How to Run

Clone the repository:

```bash
git clone https://github.com/Shreyashmoon123/ASPNetCore-JWT-Authentication.git
```

Navigate to the project:

```bash
cd ASPNetCore-JWT-Authentication
```

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations if required:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

Then open the Swagger URL shown in the terminal.

## 📂 Project Structure

```text
AuthAPI
│
├── Controller
│   └── UserAuthController.cs
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Models
│   ├── ApplicationUser.cs
│   ├── LoginModel.cs
│   └── RegisterModel.cs
│
├── Migrations
│
├── Program.cs
├── appsettings.json
└── AuthAPI.csproj
```

## 🎯 Learning Objectives

This project was built to understand and implement:

* ASP.NET Core Identity
* JWT Authentication
* JWT Token Generation
* Claims
* Role-Based Authorization
* Password Validation
* Authentication vs Authorization
* Protected API Endpoints
* Entity Framework Core
* SQL Server Integration
* Swagger API Testing

## 👨‍💻 Author

**Shreyash Katiyar**

.NET Developer | ASP.NET Core | C# | Web API | MVC | EF Core | SQL Server

---

⭐ If you find this project useful, feel free to explore the repository.
