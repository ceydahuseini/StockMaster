using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace StockMaster.Services
{
    public class SaleService : ISaleService
    {
        private readonly StockDbContext _context;

        public SaleService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.User)
                .Include(s => s.Warehouse)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .OrderByDescending(s => s.DateTime)
                .ToListAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.User)
                .Include(s => s.Warehouse)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.SaleId == id);
        }

        public async Task<bool> CreateSaleAsync(Sale sale)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                
                foreach (var item in sale.SaleItems)
                {
                    var stock = await _context.WarehouseStocks
                        .FirstOrDefaultAsync(ws => ws.WarehouseId == sale.WarehouseId && ws.ProductId == item.ProductId);

                    if (stock == null || stock.QuantityOnHand < item.Quantity)
                    {
                        throw new Exception("Insufficient stock");
                    }

                    stock.QuantityOnHand -= item.Quantity;
                    stock.LastUpdated = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Sales.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(s => s.DateTime >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(s => s.DateTime <= endDate.Value);

            return await query.SumAsync(s => (decimal?)s.TotalAmount) ?? 0;
        }
    }
}