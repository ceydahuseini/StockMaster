# 📦 StockMaster

StockMaster is a web-based inventory and sales management platform that enables businesses to manage their stock and sales processes through a single system. The system tracks product inflows and outflows in real time, records sales transactions, and automatically updates stock quantities. Users can manage suppliers, add products, and receive low-stock notifications.

---

## 🚀 Features

- 📊 **Real-time Inventory Tracking** - Monitor stock levels across multiple warehouses
- 🛒 **Sales Management** — Record and manage sales transactions
- 🏭 **Purchase Orders** — Create and track purchase orders from suppliers
- 🏬 **Multi-Warehouse Support** — Manage stock across different warehouse locations
- 👥 **User & Role Management** — Admin, Inventory Manager, Sales Personnel, Warehouse Staff roles
- 🤝 **Supplier Management** — Add and manage supplier information
- 👤 **Customer Management** — Track customer data and purchase history
- 🗂️ **Category Management** — Organize products into categories
- ⚠️ **Low Stock Alerts** — Automatic reorder level notifications

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC 8.0 |
| Language | C# |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Frontend | HTML, CSS, Bootstrap |
| IDE | Visual Studio 2022 |

---

## 🗂️ Project Structure

```
StockMaster/
├── Controllers/        # MVC Controllers
├── Data/               # DbContext and database configuration
├── Migrations/         # EF Core migrations
├── Models/             # Entity models
├── Services/           # Business logic layer
├── ViewModels/         # View-specific models
├── Views/              # Razor views
├── wwwroot/            # Static files (CSS, JS, images)
├── appsettings.json    # App configuration
└── Program.cs          # Entry point
```

---

## 🗄️ Database Schema

The database consists of the following tables:

- **users** — System users with roles
- **customer** — Customer records
- **category** — Product categories
- **supplier** — Supplier information
- **product** — Product catalog (linked to category & supplier)
- **warehouse** — Warehouse locations
- **warehouse_stock** — Stock levels per product per warehouse
- **sale** — Sales transactions
- **sale_item** — Individual items in a sale
- **purchase_order** — Orders placed to suppliers
- **purchase_order_item** — Individual items in a purchase order

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/ceydahuseini/StockMaster.git
   cd StockMaster
   ```

2. **Configure the database connection**

   Update `appsettings.json` with your PostgreSQL connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=stock_management;Username=your_user;Password=your_password"
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```
   Or press **F5** in Visual Studio.

---

## 👤 Default Users

The system includes the following roles:

| Role |
|---|
| Admin |
| Inventory Manager |
| Sales Personnel |
| Warehouse Staff |

## 🗄️ Database Scripts

- 📄 [DDL - Database Schema](./DDL.sql)
- 📄 [DML - Sample Data](./DML.sql)

### 🧪 Test Account

| Username | Password | Role |
|---|---|---|
| test_admin | admin123 | Admin |

> ⚠️ **Note:** Default passwords are for development only. Change them before deploying to production.

---

## 📄 License

This project was developed for educational purposes.

---

## 👩‍💻 Developer

**Ceyda Huseini** — [GitHub](https://github.com/ceydahuseini)
