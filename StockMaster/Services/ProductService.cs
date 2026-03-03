using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace StockMaster.Services
{
    public class ProductService : IProductService
    {
        private readonly StockDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(StockDbContext context)
        {
            _context = context;
        }

        public ProductService(StockDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateProductWithUserAsync(Product product, string username)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                await _context.Database.ExecuteSqlRawAsync($"SELECT set_config('app.current_user', '{username}', false)");

                
                _context.Update(product);

                
                var result = await _context.SaveChangesAsync() > 0;

                
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }        

        public async Task<bool> DeleteProductAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product != null)
                {
                    product.IsActive = false;
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        

        public async Task<List<Product>> GetLowStockProductsAsync()
        {
            var query = from p in _context.Products
                        join ws in _context.WarehouseStocks on p.ProductId equals ws.ProductId
                        group ws by new { p.ProductId, p.Name, p.ReorderLevel } into g
                        where g.Sum(x => x.QuantityOnHand) <= g.Key.ReorderLevel
                        select new Product
                        {
                            ProductId = g.Key.ProductId,
                            Name = g.Key.Name,
                            ReorderLevel = g.Key.ReorderLevel
                        };

            return await query.ToListAsync();
        }
    }
}