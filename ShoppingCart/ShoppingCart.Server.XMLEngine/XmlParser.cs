using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ShoppingCart.Server.XMLEngine.Entities;
using ShoppingCart.Server.XMLEngine.Parsers;

namespace ShoppingCart.Server.XMLEngine
{
    public class XmlParser
    {
        public List<Product> ParseFile(string fileName, IUnitsParser unitsParser, string root, ShopInfo shop)
        {
            var categorizer = new Categorizer();
            var doc = new XmlDocument();
            doc.Load(fileName);

            var nodes = doc.DocumentElement?.SelectNodes(root);

            var products = new List<Product>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    var unitsQuantity = unitsParser.GetUnitsQuantity(node);
                    var product = new Product()
                    {
                        ProductId = node.SelectSingleNode("ItemCode")?.InnerText,
                        Name = node.SelectSingleNode("ItemName")?.InnerText,
                        Price = float.Parse(node.SelectSingleNode("ItemPrice")?.InnerText),
                        ManufactureCountry = node.SelectSingleNode("ManufactureCountry")?.InnerText,
                        ManufactureName = node.SelectSingleNode("ManufactureName")?.InnerText,
                        Quantity = unitsQuantity.Quantity,
                        Units = unitsQuantity.Units,
                        Shop = shop,
                    };
                    product.Category = categorizer.GetCategoryForProduct(product);
                    products.Add(product);
                }
            }
            return products;
        }
    }
}
