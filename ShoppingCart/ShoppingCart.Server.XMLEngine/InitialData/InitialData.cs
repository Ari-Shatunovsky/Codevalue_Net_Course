using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.InitialData
{
    public class InitialData
    {
        public static ICollection<ShopInfo> Shops = new List<ShopInfo>()
        {
            new ShopInfo() {Name = "Victory Lod", BranchId = 10, Brand = ShopBrand.Victory},
            new ShopInfo() {Name = "YBitan Ashkelon", BranchId = 1, Brand = ShopBrand.YBitan},
            new ShopInfo() {Name = "Coob Jerusaleem", BranchId = 18, Brand = ShopBrand.Coob}
        };
    }
}
