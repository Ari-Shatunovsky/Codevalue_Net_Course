using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(ShoppingCartWeb.Startup))]

namespace ShoppingCartWeb
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
