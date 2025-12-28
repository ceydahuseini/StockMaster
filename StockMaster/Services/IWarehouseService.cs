using System.Collections.Generic;
using System.Threading.Tasks;
using StockMaster.Models;

namespace StockMaster.Services
{
    public interface IWarehouseService
    {
        Task<List<WarehouseStock>> GetWarehouseStockAsync(int warehouseId);
        Task<Dictionary<string, int>> GetStockSummaryAsync();
    }
}