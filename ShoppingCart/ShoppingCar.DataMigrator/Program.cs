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

namespace ShoppingCar.DataMigrator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ProgramAsync().Wait();
        }

        private static async Task ProgramAsync()
        {
            var ctx = new ProductContext();
            var repository = new ProductsRepository(ctx);

            await repository.AddShopsAsync(InitialData.Shops);

            var shops = await repository.GetShopsAsync();
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

            victoryProducts = victoryProducts.ToList();
            ybitanProducts = ybitanProducts.ToList();
            coobProducts = coobProducts.ToList();

            await repository.AddCategoiesAsync(Categorizer.Categories);

            await repository.AddProductsAsync(victoryProducts);
            await repository.AddProductsAsync(ybitanProducts);
            await repository.AddProductsAsync(coobProducts);

            Task.WaitAll(new ProductsRepository(new ProductContext()).FindAndAddSimilarProductsAsync(victoryInfo),
                new ProductsRepository(new ProductContext()).FindAndAddSimilarProductsAsync(ybitanInfo),
                new ProductsRepository(new ProductContext()).FindAndAddSimilarProductsAsync(coobInfo));
        }
    }
}
