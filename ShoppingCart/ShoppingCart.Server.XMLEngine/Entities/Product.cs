using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Server.XMLEngine.Entities
{
    public class Product
    {
        public Product()
        {
            SimilarProducts = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public string ProductId { set; get; }
        public string Name { set; get; }
        public string ManufactureName { set; get; }
        public string ManufactureCountry { set; get; }
        public float Price { set; get; }
        public Units Units { set; get; }
        public float Quantity { set; get; }
        public ICollection<Product> SimilarProducts { set; get; }

        public float PricePerUnit => Price/Quantity;

        public ProductCategory Category { set; get; }
        public ShopInfo Shop { set; get; }

        public override string ToString()
        {
            return $"\nId: {Id} \nName: {Name} \nPrice: {Price} \nQuanitity: {Quantity} \nPrice for unit: {PricePerUnit}";
        }
    }
}
