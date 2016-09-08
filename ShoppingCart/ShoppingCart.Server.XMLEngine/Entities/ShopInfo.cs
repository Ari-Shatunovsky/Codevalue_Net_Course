using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Server.XMLEngine.Entities
{
    public class ShopInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ShopBrand Brand { get; set; }
        public int BranchId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
