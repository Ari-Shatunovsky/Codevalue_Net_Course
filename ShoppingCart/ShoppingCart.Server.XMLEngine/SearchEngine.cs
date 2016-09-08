using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine
{
    public class SearchEngine
    {
        public ICollection<Product> Search(ICollection<Product> products, string searchTerm)
        {
            return products.Where(p => p.Name.Contains(searchTerm)).ToList();
        } 
    }
}