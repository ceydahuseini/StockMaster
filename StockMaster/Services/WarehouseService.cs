using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace StockMaster.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly StockDbContext _context;

        public WarehouseService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<WarehouseStock>> GetWarehouseStockAsync(int warehouseId)
        {
            return await _context.WarehouseStocks
                .Include(ws => ws.Product)
                .Include(ws => ws.Warehouse)
                .Where(ws => ws.WarehouseId == warehouseId)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetStockSummaryAsync()
        {
            var totalProducts = await _context.Products.CountAsync(p => p.IsActive);
            var totalStock = await _context.WarehouseStocks.SumAsync(ws => ws.QuantityOnHand);
            var warehouses = await _context.Warehouses.CountAsync();

            return new Dictionary<string, int>
            {
                { "TotalProducts", totalProducts },
                { "TotalStock", totalStock },
                { "Warehouses", warehouses }
            };
        }
    }
}