using System.Xml;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Parsers
{
    public class CoobUnitsParser : IUnitsParser
    {
        public UnitsQuantity GetUnitsQuantity(XmlNode node)
        {
            var result = new UnitsQuantity()
            {
                Quantity = 1,
                Units = Units.Unit
            };
            var units = node.SelectSingleNode("UnitOfMeasure")?.InnerText;
            var quantity = node.SelectSingleNode("Quantity")?.InnerText;

            float.TryParse(quantity, out result.Quantity);

            if (units.Contains("ק\"ג"))
            {
                result.Units = Units.Kilogramm;
            }
            else if (units.Contains("גרם"))
            {
                result.Units = Units.Kilogramm;
                result.Quantity /= 1000;
            }
            else if (units.Contains("ליטר"))
            {
                result.Units = Units.Liter;
            }
            else if (units.Contains("מ\"ל"))
            {
                result.Units = Units.Liter;
                result.Quantity /= 1000;
            }
            return result;
        }
    }
}