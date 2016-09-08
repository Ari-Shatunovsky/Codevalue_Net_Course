using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Relational
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductContext _ctx;
        private readonly SimilarProductSearchEngine _similarSearch;

        public ProductsRepository(ProductContext ctx)
        {
            _ctx = ctx;
            _similarSearch = new SimilarProductSearchEngine();
        }

        public ICollection<ShopInfo> GetShops()
        {
            return _ctx.Shops.ToList();
        }

        public void AddProducts(ICollection<Product> products)
        {
            Console.WriteLine($"Adding {products.Count} products.");
            _ctx.Products.AddRange(products);
            _ctx.SaveChanges();
        }

        public void AddCategoies(ICollection<ProductCategory> productCategories)
        {
            _ctx.ProductCategories.AddRange(productCategories);
            _ctx.SaveChanges();
        }

        public void AddShops(ICollection<ShopInfo> shops)
        {
            _ctx.Shops.AddRange(shops);
            _ctx.SaveChanges();
        }

        public void FindAndAddSimilarProducts(ShopInfo shop)
        {
            var products = _ctx.Products.Where(p => p.Shop.Id == shop.Id);
            var shops = _ctx.Shops.Where(s => s.Id != shop.Id).ToList();
            var fullCarts = shops.Select(s => new Cart() {Products = _ctx.Products.Where(p => p.Shop.Id == s.Id).ToList(), Shop = s}).ToList();

            foreach (var product in products)
            {
                Console.WriteLine($"Find similar products for {product.Name} products.");
                product.SimilarProducts = new List<Product>();
                foreach (var similar in fullCarts.Select(fc => _similarSearch.Search(fc.Products, product)).Where(similar => similar != null))
                {
                    product.SimilarProducts.Add(similar);
                }
            }
            _ctx.Products.AddOrUpdate(products.ToArray());
            _ctx.SaveChanges();
            Console.WriteLine($"Finishing to add similar products for {shop.Name}");
        }

        public ICollection<Product> GetProductsByCategory(int categoryId)
        {
            return _ctx.Products.Where(c => c.Category.Id == categoryId).ToList();
        }

        public ICollection<Product> SearchProductByName(int shopId, string searchTerm)
        {
            char[] delimiters = { ',', ';', ' ', '-' };
            var words = searchTerm.Split(delimiters);
            var wordsLength = words.Length;
            return _ctx.Products.Where(p => p.Shop.Id == shopId && words.Count(w => p.Name.Contains(w)) == wordsLength).ToList();
        }

        public ICollection<Cart> GetRandomCarts()
        {
            var rand = new Random();
            var searchEngine = new SimilarProductSearchEngine();
            var cartSize = 5;
            var victoryShopInfo = _ctx.Shops.FirstOrDefault(s => s.Brand == ShopBrand.Victory);
            var ybitanShopInfo = _ctx.Shops.FirstOrDefault(s => s.Brand == ShopBrand.YBitan);
            var coobShopInfo = _ctx.Shops.FirstOrDefault(s => s.Brand == ShopBrand.Coob);
            var result = new List<Cart>()
            {
                new Cart() {Products = new List<Product>(), Shop = victoryShopInfo },
                new Cart() {Products = new List<Product>(), Shop = ybitanShopInfo },
                new Cart() {Products = new List<Product>(), Shop = coobShopInfo },
            };
            var victoryProducts = _ctx.Products.Where(p => p.Shop.Brand == ShopBrand.Victory).ToList();
            var ybitanProducts = _ctx.Products.Where(p => p.Shop.Brand == ShopBrand.YBitan).ToList();
            var coobProducts = _ctx.Products.Where(p => p.Shop.Brand == ShopBrand.Coob).ToList();
            for (var i = 0; i < cartSize; i++)
            {
                var victoryProduct = victoryProducts[rand.Next(victoryProducts.Count)];
                var ybitanProduct = searchEngine.Search(ybitanProducts, victoryProduct);
                var coobProduct = searchEngine.Search(coobProducts, victoryProduct);

                result[0].Products.Add(victoryProduct);
                result[1].Products.Add(ybitanProduct);
                result[2].Products.Add(coobProduct);
            }
            return result;
        }

        public ICollection<Cart> GetSimilarProducts(Cart cart, ICollection<ShopInfo> shops)
        {
            var result = new List<Cart>();
            var products = cart.Products.Select(p => _ctx.Products.Include(c => c.SimilarProducts).Include(c => c.Shop).FirstOrDefault(cp => cp.Id == p.Id)).ToList();
            foreach (var shop in shops)
            {
                var newCart = new Cart() {Products = new List<Product>(), Shop = shop};
                foreach (var product in products)
                {
                    var similarProducts =
                        product.SimilarProducts.Select(p => _ctx.Products.Include(sp => sp.Shop).FirstOrDefault(sp => sp.Id == p.Id));
                    newCart.Products.Add(similarProducts.FirstOrDefault(sp => sp.Shop.Id == shop.Id));
                }
                result.Add(newCart);
            }
            return result;
        }
    }
}
