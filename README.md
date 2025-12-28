# Stock Master

Inventory management system built with ASP.NET Core 8.0 and PostgreSQL.

## Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)

## Installation

### 1. Clone the Project

```bash
git clone https://github.com/ceydahuseini/StockMaster.git
cd StockMaster
```

### 2. Setup Database

Create database and run SQL scripts:

```bash
# Login to PostgreSQL
psql -U postgres

# Create database
CREATE DATABASE stock_db;
\q

# Run scripts
psql -U postgres -d stock_db -f schema_creation.sql
psql -U postgres -d stock_db -f data_load.sql
```

### 3. Configure Database Connection

Edit `appsettings.json` and update your PostgreSQL password:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=stock_db;Username=postgres;Password=YOUR_PASSWORD;SearchPath=stock_management"
  }
}
```

### 4. Install Dependencies and Run

```bash
dotnet restore
dotnet run
```

### 5. Open in Browser

Navigate to: `https://localhost:5001`

## Login

- **Username:** `test_admin`
- **Password:** `admin123`

## Features

- Dashboard with real-time statistics
- Product management (CRUD operations)
- Sales management with auto stock updates
- Purchase orders
- Multi-warehouse support
- Customer and supplier management
- Role-based access control

## Troubleshooting

**Database connection error?**
- Ensure PostgreSQL is running
- Verify password in `appsettings.json`
- Check that `SearchPath=stock_management` is included

**Start PostgreSQL:**
```bash
# Windows
net start postgresql-x64-14

# Linux
sudo systemctl start postgresql

# Mac
brew services start postgresql
```

## Project Structure

```
StockMaster/
├── Controllers/          # MVC Controllers
├── Models/               # Database entities
├── Views/                # UI pages
├── Services/             # Business logic
├── schema_creation.sql   # Database schema
├── data_load.sql         # Sample data
└── appsettings.json      # Configuration
```
