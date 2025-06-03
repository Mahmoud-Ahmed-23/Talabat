
# 🍔 Talabat API - E-Commerce Platform

**Talabat** is a backend e-commerce web API built using ASP.NET Core, applying Clean Architecture and Onion Architecture principles. The system provides complete functionality for managing products, orders, users, and payments with flexibility and scalability in mind.

---

## 🧱 Project Structure

```bash
Talabat/
├── LinkDev.Talabat.APIs/           # Presentation Layer (Controllers, Swagger, Middleware)
├── LinkDev.Talabat.Application/    # Application Layer (DTOs, Interfaces, Services)
├── LinkDev.Talabat.Domain/         # Domain Layer (Entities, Enums, Specifications)
├── LinkDev.Talabat.Infrastructure/ # Infrastructure Layer (EF Core, Repositories, Stripe, Caching)
├── LinkDev.Talabat.sln             # Solution File
└── README.md
```

---

## 🚀 Features

### 📦 Product Management

- CRUD operations on products.
- Products are categorized and manageable by admins.
- Supports price, stock, and brand management.

### 🛒 Order Management

- Customers can place orders.
- Tracks order status: Pending, Processing, Shipped, Delivered.
- Admins can manage and update order statuses.

### 👥 User Management

- Registration & login using ASP.NET Identity.
- Role-based access: Admins, Customers.
- JWT-based secure authentication.

### 💳 Payment Integration

- Integrated with **Stripe** for online payments.
- Payment intents and transaction tracking.
- Refund support on order cancellation.

### 🧠 Business Patterns

- **Generic Repository & Unit of Work**.
- **Specification Pattern** for flexible querying.
- **Result Pattern** for clean API responses.

### 🚀 Caching & Performance

- **Redis** integrated to cache heavy data (e.g., product lists).
- Improves response time and reduces database hits.

---

## 🛠️ Technologies Used

| Tech               | Purpose                          |
|--------------------|----------------------------------|
| ASP.NET Core 8     | API Framework                    |
| Entity Framework   | ORM for SQL Server               |
| SQL Server         | Primary relational database      |
| AutoMapper         | Entity ↔ DTO Mapping             |
| FluentValidation   | Validation for incoming DTOs     |
| Swagger / Postman  | API Testing & Documentation      |
| JWT + Identity     | Authentication & Authorization   |
| Stripe             | Payment Gateway Integration      |
| Redis              | Caching Layer                    |
| Hangfire           | Background Job Scheduler         |

---

## 🔧 Getting Started

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Mahmoud-Ahmed-23/Talabat.git
cd Talabat
```

### 2️⃣ Configure Database

- Update your connection string in `appsettings.json`.
- Apply the latest EF Core migrations:

```bash
dotnet ef database update
```

### 3️⃣ Run the Project

```bash
dotnet run --project LinkDev.Talabat.APIs
```

Navigate to:

```
https://localhost:{PORT}/swagger
```

You will find the Swagger UI to explore the API endpoints.

---

## 📬 Contact

- 📧 Email:    ma6031774@gmail.com 
- 💼 LinkedIn: [Mahmoud Ahmed](https://www.linkedin.com/in/mahmoud-ahmed-abdeltwab?lipi=urn%3Ali%3Apage%3Ad_flagship3_profile_view_base_contact_details%3BiNtqVwH0Rp6SSbr63aA05A%3D%3D)
