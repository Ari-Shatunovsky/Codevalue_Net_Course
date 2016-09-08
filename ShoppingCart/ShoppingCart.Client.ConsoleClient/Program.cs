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
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"../../../ShoppingCart.Server.XMLEngine/Xml/hereIam.txt");
            var searchEngine = new SimilarProductSearchEngine();

            //Victory
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
            victoryProducts = victoryProducts.Take(100).ToList();
            ybitanProducts = ybitanProducts.Take(100).ToList();
            coobProducts = coobProducts.Take(100).ToList();
            repository.AddCategoies(Categorizer.Categories);
            repository.AddProducts(victoryProducts);
            repository.AddProducts(ybitanProducts);
            repository.AddProducts(coobProducts);
            repository.FindAndAddSimilarProducts(victoryInfo);
            repository.FindAndAddSimilarProducts(ybitanInfo);
            repository.FindAndAddSimilarProducts(coobInfo);
            {
                
            }
            //            var ybProducts = parser.ParseFile("../../../ShoppingCart.Server.XMLEngine/Xml/YbitanSample.Xml", 
            //                new YbUnitsParser(), "/Root/Items/Item");
            //            var victoryProducts = parser.ParseFile("../../../ShoppingCart.Server.XMLEngine/Xml/VictorySample.Xml", 
            //                new VictoryUnitsParser(), "/Prices/Products/Product");
            //            var rnd = new Random();
            //            var ybproduct = ybProducts[rnd.Next(ybProducts.Count())];
            //            file.WriteLine($"YB Product: {ybproduct}");
            //            var victoryProduct = searchEngine.SearchById(victoryProducts, ybproduct);
            //            file.WriteLine($"Victory Product: {victoryProduct}");
            //            var similarVictoryProducts = searchEngine.SearchByName(victoryProducts, ybproduct);
            //            foreach (var product in similarVictoryProducts)
            //            {
            //                file.WriteLine($"Victory Similar products: {product}");
            //            }
            //            file.Close();
            //            var breads = ybProducts.Where(p => p.Category == Categorizer.Bread).ToList();
            //            var meat = ybProducts.Where(p => p.Category == Categorizer.Meat).ToList();
            //            var alcohol = ybProducts.Where(p => p.Category == Categorizer.Alcohol).ToList();
            //            Console.ReadLine();
        }
    }
}
