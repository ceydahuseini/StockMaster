using Microsoft.EntityFrameworkCore;
using StockMaster.Models;
using StockMaster.ViewModels;
using Microsoft.AspNetCore.Http;

namespace StockMaster.Data
{
    public class StockDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StockDbContext(DbContextOptions<StockDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
        public DbSet<ProductPriceLog> ProductPriceLogs { get; set; }

        public DbSet<VwSalesByDay> VwSalesByDays { get; set; }
        public DbSet<VwEmployeeRanking> VwEmployeeRankings { get; set; }
        public DbSet<VwTodaysSummary> VwTodaysSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SaleItem>().HasKey(si => new { si.SaleId, si.ProductId });
            modelBuilder.Entity<PurchaseOrderItem>().HasKey(poi => new { poi.PoId, poi.ProductId });
            modelBuilder.Entity<WarehouseStock>().HasKey(ws => new { ws.WarehouseId, ws.ProductId });

            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasPrecision(12, 2);
            modelBuilder.Entity<Sale>().Property(s => s.TotalAmount).HasPrecision(15, 2);
            modelBuilder.Entity<SaleItem>().Property(si => si.UnitPriceAtSale).HasPrecision(12, 2);
            modelBuilder.Entity<PurchaseOrderItem>().Property(poi => poi.UnitCost).HasPrecision(12, 2);

            modelBuilder.Entity<ProductPriceLog>().ToTable("product_price_log", "stock_management");

            modelBuilder.Entity<VwSalesByDay>().HasNoKey().ToView("vw_sales_by_day_of_week", "stock_management");
            modelBuilder.Entity<VwEmployeeRanking>().HasNoKey().ToView("vw_employee_sales_ranking", "stock_management");
            modelBuilder.Entity<VwTodaysSummary>().HasNoKey().ToView("vw_todays_sales_summary", "stock_management");

           
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "system";

            await Database.ExecuteSqlRawAsync("SELECT set_config('app.current_user', {0}, false)", new[] { username }, cancellationToken);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}