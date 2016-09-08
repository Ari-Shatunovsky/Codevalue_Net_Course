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
        ICollection<Product> GetProductsByCategory(int categoryId);
        ICollection<Product> SearchProductByName(int shopId, string searchTerm);
        ICollection<Cart> GetRandomCarts();
        ICollection<Cart> GetSimilarProducts(Cart cart, ICollection<ShopInfo> shops );
        ICollection<ShopInfo> GetShops();
        void AddProducts(ICollection<Product> products);
        void AddCategoies(ICollection<ProductCategory> productCategories);
        void AddShops(ICollection<ShopInfo> shops);
        void FindAndAddSimilarProducts(ShopInfo shop);
    }

}
