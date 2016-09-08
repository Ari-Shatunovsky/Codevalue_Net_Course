using System.Linq;
using ShoppingCart.Server.XMLEngine.Parsers;

namespace ShoppingCart.Server.XMLEngine.Relational
{
    public class ProductDataSeeder
    {
        private readonly ProductContext _ctx;

        public ProductDataSeeder(ProductContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {
//            if (_ctx.Products.Any())
//            {
//                return;
//            }
//            _ctx.ProductCategories.AddRange(Categorizer.Categories);
//            var parser = new XmlParser();
//            var victoryProducts = parser.ParseFile("../../../ShoppingCart.Server.XMLEngine/Xml/VictorySample.Xml",
//                new VictoryUnitsParser(), "/Prices/Products/Product");
//            _ctx.Products.AddRange(victoryProducts);
        }
    }
}