using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMaster.Models;

namespace StockMaster.Services
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAllSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
        Task<bool> CreateSaleAsync(Sale sale);
        Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}