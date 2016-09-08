using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Server.XMLEngine.Entities
{


    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { set; get; }
        //public static ProductCategory
        public virtual ICollection<Product> Products { get; set; }
    }

    
}
