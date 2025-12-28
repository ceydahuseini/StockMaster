using System.Collections.Generic;
using System.Threading.Tasks;
using StockMaster.Models;

namespace StockMaster.Services
{
    public interface IPurchaseOrderService
    {
        Task<List<PurchaseOrder>> GetAllPurchaseOrdersAsync();
        Task<PurchaseOrder> GetPurchaseOrderByIdAsync(int id);
        Task<bool> CreatePurchaseOrderAsync(PurchaseOrder po);
        Task<bool> ReceivePurchaseOrderAsync(int poId);
    }
}