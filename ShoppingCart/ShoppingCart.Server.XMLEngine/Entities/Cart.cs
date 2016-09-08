using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Server.XMLEngine.Entities
{
    public class Cart
    {
        public ICollection<Product> Products { set; get; }
        public ShopInfo Shop { set; get; }
    }
}
