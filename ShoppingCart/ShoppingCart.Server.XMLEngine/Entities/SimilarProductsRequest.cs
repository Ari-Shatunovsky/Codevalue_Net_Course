using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Server.XMLEngine.Entities
{
    public class SimilarProductsRequest
    {
        public Cart Cart { set; get; }
        public ICollection<ShopInfo> Shops { set; get; }
    }
}
