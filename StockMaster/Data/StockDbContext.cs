using Microsoft.EntityFrameworkCore;
using StockMaster.Models;

namespace StockMaster.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<SaleItem>()
                .HasKey(si => new { si.SaleId, si.ProductId });

            modelBuilder.Entity<PurchaseOrderItem>()
                .HasKey(poi => new { poi.PoId, poi.ProductId });

            modelBuilder.Entity<WarehouseStock>()
                .HasKey(ws => new { ws.WarehouseId, ws.ProductId });

            
            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.UnitPriceAtSale)
                .HasPrecision(12, 2);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(poi => poi.UnitCost)
                .HasPrecision(12, 2);
        }
    }
}