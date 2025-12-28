using System.Collections.Generic;
using System.Threading.Tasks;
using StockMaster.Models;

namespace StockMaster.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<List<Product>> GetLowStockProductsAsync();
    }
}