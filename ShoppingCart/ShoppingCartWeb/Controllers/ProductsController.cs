using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
//    [EnableCors(origins: "*", headers: "*", methods: "get,post")]
    public class ProductsController : ApiControllerBase
    {
        // GET: Product
        public HttpResponseMessage GetByCategory(int id)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetProductsByCategory(id));
        }

        [System.Web.Http.Route("api/products/randomcarts")]
        public HttpResponseMessage GetRandomCarts()
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetRandomCarts());
        }

        [System.Web.Http.Route("api/products/emptycarts")]
        public HttpResponseMessage GetEmptyCarts()
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetEmptyCarts());
        }

        [System.Web.Http.Route("api/products/similar")]
        public HttpResponseMessage PostSimilarProducts(SimilarProductsRequest request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetSimilarProducts(request.Cart, request.Shops));
        }

        [System.Web.Http.Route("api/products/connectproducts")]
        public HttpResponseMessage PostConnectProducts(ICollection<Product> request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.SetSimilarProduct(request));
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/products/cart")]
        public HttpResponseMessage PostCart(Cart request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.AddCart(request));
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.Route("api/products/carts")]
        public HttpResponseMessage GetCarts(Cart request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetSavedCarts());
        }

        public HttpResponseMessage GetSearch(int shopId, string searchTerm)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.SearchProductByName(shopId, searchTerm));
        }
    }
}