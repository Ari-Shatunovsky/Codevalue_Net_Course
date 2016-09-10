using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Server.XMLEngine;
using ShoppingCart.Server.XMLEngine.Entities;
using ShoppingCart.Server.XMLEngine.InitialData;
using ShoppingCart.Server.XMLEngine.Parsers;
using ShoppingCart.Server.XMLEngine.Relational;

namespace ShoppingCart.Client.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new ProductContext();
            var repository = new ProductsRepository(ctx);

            repository.AddShops(InitialData.Shops);

            var shops = repository.GetShops();
            var victoryInfo = shops.First(s => s.Brand == ShopBrand.Victory);
            var ybitanInfo = shops.First(s => s.Brand == ShopBrand.YBitan);
            var coobInfo = shops.First(s => s.Brand == ShopBrand.Coob);
            var parser = new XmlParser();
            var victoryProducts = parser.ParseFile("../../../ShoppingCart.Server.XMLEngine/Xml/VictorySample.Xml",
                new VictoryUnitsParser(), "/Prices/Products/Product", victoryInfo);

            var ybitanProducts = parser.ParseFile("../../../ShoppingCart.Server.XMLEngine/Xml/YbitanSample.Xml",
                new YbUnitsParser(), "/Root/Items/Item", ybitanInfo);
            var coobProducts = parser.ParseFile("../../../ShoppingCart.Server.XMLEngine/Xml/CoobSample.Xml",
                new CoobUnitsParser(), "/root/Items/item", coobInfo);

            victoryProducts = victoryProducts.Take(500).ToList();
            ybitanProducts = ybitanProducts.Take(500).ToList();
            coobProducts = coobProducts.Take(500).ToList();

            repository.AddCategoies(Categorizer.Categories);
            repository.AddProducts(victoryProducts);
            repository.AddProducts(ybitanProducts);
            repository.AddProducts(coobProducts);

            repository.FindAndAddSimilarProducts(victoryInfo);
            repository.FindAndAddSimilarProducts(ybitanInfo);
            repository.FindAndAddSimilarProducts(coobInfo);
        }
    }
}
