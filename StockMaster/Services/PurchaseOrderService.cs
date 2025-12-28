using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMaster.Data;
using StockMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace StockMaster.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly StockDbContext _context;

        public PurchaseOrderService(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            return await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.Warehouse)
                .Include(po => po.PurchaseOrderItems)
                    .ThenInclude(poi => poi.Product)
                .OrderByDescending(po => po.OrderDate)
                .ToListAsync();
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByIdAsync(int id)
        {
            return await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.Warehouse)
                .Include(po => po.PurchaseOrderItems)
                    .ThenInclude(poi => poi.Product)
                .FirstOrDefaultAsync(po => po.PoId == id);
        }

        public async Task<bool> CreatePurchaseOrderAsync(PurchaseOrder po)
        {
            try
            {
                _context.PurchaseOrders.Add(po);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ReceivePurchaseOrderAsync(int poId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var po = await GetPurchaseOrderByIdAsync(poId);
                if (po == null || po.Status == "Received")
                    return false;

                // Update stock
                foreach (var item in po.PurchaseOrderItems)
                {
                    var stock = await _context.WarehouseStocks
                        .FirstOrDefaultAsync(ws => ws.WarehouseId == po.WarehouseId && ws.ProductId == item.ProductId);

                    if (stock == null)
                    {
                        stock = new WarehouseStock
                        {
                            WarehouseId = po.WarehouseId,
                            ProductId = item.ProductId,
                            QuantityOnHand = item.Quantity
                        };
                        _context.WarehouseStocks.Add(stock);
                    }
                    else
                    {
                        stock.QuantityOnHand += item.Quantity;
                    }

                    stock.LastUpdated = DateTime.Now;
                    item.ReceivedQuantity = item.Quantity;
                }

                po.Status = "Received";
                po.ActualDeliveryDate = DateTime.Now.Date;

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
    }
}