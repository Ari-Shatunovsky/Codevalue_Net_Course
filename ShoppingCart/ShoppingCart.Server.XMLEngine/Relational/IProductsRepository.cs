using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Relational
{
    public interface IProductsRepository
    {
        Task<ICollection<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<ICollection<Product>> SearchProductByNameAsync(int shopId, string searchTerm);
        Task<ICollection<Cart>> GetRandomCartsAsync();
        Task<ICollection<Cart>> GetSavedCartsAsync();
        Task<ICollection<Cart>> GetSimilarProductsAsync(Cart cart, ICollection<ShopInfo> shops );
        Task<ICollection<ShopInfo>> GetShopsAsync();
        Task<ICollection<Cart>> GetEmptyCartsAsync();

        Task<bool> SetSimilarProductAsync(ICollection<Product> products);
        Task<bool> AddProductsAsync(ICollection<Product> products);
        Task<bool> AddCategoiesAsync(ICollection<ProductCategory> productCategories);
        Task<bool> AddShopsAsync(ICollection<ShopInfo> shops);
        Task<bool> AddCartAsync(Cart cart);
        Task<bool> FindAndAddSimilarProductsAsync(ShopInfo shop);
    }

}
