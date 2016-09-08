using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace ShoppingCartWeb.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {

        private static readonly JsonMediaTypeFormatter JsonMediaTypeFormatter =
            new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            };

        protected new HttpResponseMessage Json<T>(T value)
        {
            return Request.CreateResponse(HttpStatusCode.OK, value, JsonMediaTypeFormatter);
        }

        protected HttpResponseMessage String(string value)
        {
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        protected new HttpResponseMessage Ok()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected HttpResponseMessage BadRequest<T>(T value)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, value, JsonMediaTypeFormatter);
        }
    }
}
