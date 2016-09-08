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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiControllerBase
    {
        // GET: Product
        public HttpResponseMessage GetByCategory(int id)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetProductsByCategory(id));
        }

        public HttpResponseMessage GetCarts()
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetRandomCarts());
        }

        //        [System.Web.Http.HttpPost]
        //        [System.Web.Http.AcceptVerbs("OPTIONS")]
//        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage PostSimilarProducts(SimilarProductsRequest request)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.GetSimilarProducts(request.Cart, request.Shops));
        }

        public HttpResponseMessage GetSearch(int shopId, string searchTerm)
        {
            IProductsRepository repository = new ProductsRepository(new ProductContext());
            return Json(repository.SearchProductByName(shopId, searchTerm));
        }

    }
}