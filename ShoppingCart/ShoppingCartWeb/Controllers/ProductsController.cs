using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ShoppingCart.Server.XMLEngine.Entities;
using ShoppingCart.Server.XMLEngine.Relational;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ShoppingCartWeb.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        // GET: Product
        public async Task<HttpResponseMessage> GetByCategory(int id)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.GetProductsByCategoryAsync(id));
        }

        [System.Web.Http.Route("api/products/randomcarts")]
        public async Task<HttpResponseMessage> GetRandomCarts()
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.GetRandomCartsAsync());
        }

        [System.Web.Http.Route("api/products/emptycarts")]
        public async Task<HttpResponseMessage> GetEmptyCarts()
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.GetEmptyCartsAsync());
        }

        [System.Web.Http.Route("api/products/similar")]
        public async Task<HttpResponseMessage> PostSimilarProducts(SimilarProductsRequest request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.GetSimilarProductsAsync(request.Cart, request.Shops));
        }

        [System.Web.Http.Route("api/products/connectproducts")]
        public async Task<HttpResponseMessage> PostConnectProducts(ICollection<Product> request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.SetSimilarProductAsync(request));
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/products/cart")]
        public async Task<HttpResponseMessage> PostCart(Cart request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.AddCartAsync(request));
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.Route("api/products/carts")]
        public async Task<HttpResponseMessage> GetCarts(Cart request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await  repository.GetSavedCartsAsync());
        }

        public async Task<HttpResponseMessage> GetSearch(int shopId, string searchTerm)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(await repository.SearchProductByNameAsync(shopId, searchTerm));
        }
    }
}