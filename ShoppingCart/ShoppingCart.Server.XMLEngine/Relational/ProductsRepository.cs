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

        public async Task<bool> SetSimilarProductAsync(ICollection<Product> products)
        {
            var prodArray = products.ToArray();
            var originalId = prodArray[0].Id;
            var similarId = prodArray[1].Id;
            var originalProduct =
                await _ctx.Products.Include(c => c.SimilarProducts)
                    .Include(c => c.Shop)
                    .FirstOrDefaultAsync(cp => cp.Id == originalId);
            var similarProduct =
                await _ctx.Products.Include(c => c.SimilarProducts)
                    .Include(c => c.Shop)
                    .FirstOrDefaultAsync(cp => cp.Id == similarId);
            var originalProductSp = originalProduct.SimilarProducts.ToList();
            var similarProductSp = similarProduct?.SimilarProducts.ToList() ?? new List<Product>();

            var indexOp = originalProductSp.FindIndex(p => p.Id == similarProduct.Id);
            var indexSp = similarProductSp.FindIndex(p => p.Id == originalProduct.Id);
            if (indexOp >= 0)
            {
                originalProductSp[indexOp] = similarProduct;
            }
            else
            {
                originalProductSp.Add(similarProduct);
            }
            originalProduct.SimilarProducts = originalProductSp;

            if (indexSp >= 0)
            {
                similarProductSp[indexOp] = originalProduct;
            }
            else
            {
                similarProductSp.Add(originalProduct);
            }
            similarProduct.SimilarProducts = similarProductSp;

            _ctx.Products.AddOrUpdate(originalProduct);
            _ctx.Products.AddOrUpdate(similarProduct);
            await _ctx.SaveChangesAsync();
            return true;

        }

        public async Task<ICollection<ShopInfo>> GetShopsAsync()
        {
            return await _ctx.Shops.ToListAsync();
        }

        public async Task<bool> AddProductsAsync(ICollection<Product> products)
        {
            Console.WriteLine($"Adding {products.Count} products.");
            _ctx.Products.AddRange(products);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCategoiesAsync(ICollection<ProductCategory> productCategories)
        {
            _ctx.ProductCategories.AddRange(productCategories);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddShopsAsync(ICollection<ShopInfo> shops)
        {
            _ctx.Shops.AddRange(shops);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCartAsync(Cart cart)
        {
            var shop = await _ctx.Shops.FirstOrDefaultAsync(s => s.Id == cart.Shop.Id);
            cart.Shop = shop;
            var productIds = cart.Products.Select(p => p.Id);
            var products = await _ctx.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            cart.Products = products;
            _ctx.Carts.AddOrUpdate(cart);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FindAndAddSimilarProductsAsync(ShopInfo shop)
        {
            var products = await _ctx.Products.Where(p => p.Shop.Id == shop.Id).ToListAsync();
            var shops = await _ctx.Shops.Where(s => s.Id != shop.Id).ToListAsync();
            var fullCarts = shops.Select(s => new Cart() {Products = _ctx.Products.Where(p => p.Shop.Id == s.Id).ToList(), Shop = s}).ToList();
            var i = 0;
            var count = products.Count();
            foreach (var product in products)
            {
                i ++;
                Console.WriteLine($"Find similar products for {i} of {count} products.");
                product.SimilarProducts = new List<Product>();
                foreach (var similar in fullCarts.Select(fc => _similarSearch.Search(fc.Products, product)).Where(similar => similar != null))
                {
                    product.SimilarProducts.Add(similar);
                }
                _ctx.Products.AddOrUpdate(product);
            }
            
            await _ctx.SaveChangesAsync();
            Console.WriteLine($"Finishing to add similar products for {shop.Name}");
            return true;
        }

        public async Task<ICollection<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _ctx.Products.Where(c => c.Category.Id == categoryId).ToListAsync();
        }

        public async Task<ICollection<Product>> SearchProductByNameAsync(int shopId, string searchTerm)
        {
            char[] delimiters = { ',', ';', ' ', '-' };
            var words = searchTerm.Split(delimiters);
            var wordsLength = words.Length;
            return await _ctx.Products.Include(p => p.Shop).Where(p => p.Shop.Id == shopId && words.Count(w => p.Name.Contains(w)) == wordsLength).ToListAsync();
        }

        public async Task<ICollection<Cart>> GetRandomCartsAsync()
        {
            var rand = new Random();
            var searchEngine = new SimilarProductSearchEngine();
            var cartSize = 5;
            var victoryShopInfo = await _ctx.Shops.FirstOrDefaultAsync(s => s.Brand == ShopBrand.Victory);
            var ybitanShopInfo = await _ctx.Shops.FirstOrDefaultAsync(s => s.Brand == ShopBrand.YBitan);
            var coobShopInfo = await _ctx.Shops.FirstOrDefaultAsync(s => s.Brand == ShopBrand.Coob);
            var result = new List<Cart>()
            {
                new Cart() {Products = new List<Product>(), Shop = victoryShopInfo },
                new Cart() {Products = new List<Product>(), Shop = ybitanShopInfo },
                new Cart() {Products = new List<Product>(), Shop = coobShopInfo },
            };
            var victoryProducts = await _ctx.Products.Where(p => p.Shop.Brand == ShopBrand.Victory).ToListAsync();
            var ybitanProducts = await _ctx.Products.Where(p => p.Shop.Brand == ShopBrand.YBitan).ToListAsync();
            var coobProducts = await _ctx.Products.Where(p => p.Shop.Brand == ShopBrand.Coob).ToListAsync();
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

        public async Task<ICollection<Cart>> GetEmptyCartsAsync()
        {
            var victoryShopInfo = await _ctx.Shops.FirstOrDefaultAsync(s => s.Brand == ShopBrand.Victory);
            var ybitanShopInfo = await _ctx.Shops.FirstOrDefaultAsync(s => s.Brand == ShopBrand.YBitan);
            var coobShopInfo = await _ctx.Shops.FirstOrDefaultAsync(s => s.Brand == ShopBrand.Coob);
            var result = new List<Cart>()
            {
                new Cart() {Products = new List<Product>(), Shop = victoryShopInfo },
                new Cart() {Products = new List<Product>(), Shop = ybitanShopInfo },
                new Cart() {Products = new List<Product>(), Shop = coobShopInfo },
            };
            return result;
        }

        public async Task<ICollection<Cart>> GetSavedCartsAsync()
        {
            var carts = await _ctx.Carts.Include(c => c.Shop).Include(c => c.Products).ToListAsync();
            foreach (var cart in carts)
            {
                var products =
                    cart.Products.Select( 
                        p =>
                             _ctx.Products.Include(np => np.Shop)
                                .Include(np => np.SimilarProducts)
                                .FirstOrDefault(nnp => nnp.Id == p.Id)).ToList();
                cart.Products = products;
            }
            return carts;
        }

        public async Task<ICollection<Cart>> GetSimilarProductsAsync(Cart cart, ICollection<ShopInfo> shops)
        {
            var result = new List<Cart>();
            var productIds = cart.Products.Select(p => p.Id);
            var products = await _ctx.Products.Include(c => c.SimilarProducts).Include(c => c.Shop).Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach (var shop in shops)
            {
                var newCart = new Cart() {Products = new List<Product>(), Shop = shop};
                foreach (var product in products)
                {
                    var similarProductIds = product.SimilarProducts.Select(p => p.Id);
                    var similarProduct = await  _ctx.Products.Include(p => p.Shop).FirstOrDefaultAsync(p => similarProductIds.Contains(p.Id) && p.Shop.Id == shop.Id);
                    newCart.Products.Add(similarProduct);
                }
                result.Add(newCart);
            }
            return result;
        }
    }
}
