using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Parsers
{
    public class YbUnitsParser : IUnitsParser
    {
        public UnitsQuantity GetUnitsQuantity(XmlNode node)
        {
            var result = new UnitsQuantity()
            {
                Quantity = 1,
                Units = Units.Unit
            };
            var units = node.SelectSingleNode("UnitQty")?.InnerText;
            var weighed = node.SelectSingleNode("blsWeighted")?.InnerText == "True";
            var name = node.SelectSingleNode("ItemName")?.InnerText;
            var regexMl = new Regex(@"(\d+) ?(מ)");
            var regexL = new Regex(@"((?:[1-9]\d*|0)?(?:\.\d+)) ?(ל)");
            var regexU = new Regex(@"(\d+)");
            var regexG = new Regex(@"(\d+) ?(ג`?ר?)");
            var regexKg = new Regex(@"((?:[1-9]\d*|0)?(?:\.\d+)) ?(ק)");
            if (units.Contains("ק``ג"))
            {
                result.Units = Units.Kilogramm;
                if (!weighed)
                {

                    var quantity = regexG.Match(name);
                    if (quantity.Success)
                    {
                        if (float.TryParse(quantity.Groups[1].Value, out result.Quantity))
                        {
                            result.Quantity /= 1000;
                        }
                    }
                    else
                    {
                        quantity = regexKg.Match(name);
                        if (quantity.Success)
                        {
                            float.TryParse(quantity.Groups[1].Value, out result.Quantity);
                        }
                    }
                }
            }
            else if (units.Contains("ליטר"))
            {
                result.Units = Units.Liter;

                var quantity = regexMl.Match(name);
                if (quantity.Success)
                {
                    if (float.TryParse(quantity.Groups[1].Value, out result.Quantity))
                    {
                        result.Quantity /= 1000;
                    }
                }
                else
                {
                    quantity = regexL.Match(name);
                    if (quantity.Length > 1)
                    {
                        float.TryParse(quantity.Groups[1].Value, out result.Quantity);
                    }
                }
            }
            else if (units.Contains("יחידה"))
            {
                result.Units = Units.Unit;
                var quantity = regexU.Match(name);
                float.TryParse(quantity.Value, out result.Quantity);
            }
            else
            {
                var quantity = regexG.Match(name);
                if (quantity.Length > 1)
                {
                    result.Units = Units.Kilogramm;
                    if (float.TryParse(quantity.Groups[1].Value, out result.Quantity))
                    {
                        result.Quantity /= 1000;
                    }
                }
                else
                {
                    quantity = regexKg.Match(name);
                    if (quantity.Length > 1)
                    {
                        result.Units = Units.Kilogramm;
                        float.TryParse(quantity.Groups[1].Value, out result.Quantity);
                    }
                    else
                    {
                        quantity = regexMl.Match(name);
                        if (quantity.Length > 1)
                        {
                            result.Units = Units.Liter;
                            if(float.TryParse(quantity.Groups[1].Value, out result.Quantity))
                            {
                                result.Quantity /= 1000;
                            } 
                        }
                        else
                        {
                            quantity = regexL.Match(name);
                            if (quantity.Length > 1)
                            {
                                result.Units = Units.Liter;
                                float.TryParse(quantity.Groups[1].Value, out result.Quantity);
                            }
                        }
                    }
                }
                if (result.Quantity == 0)
                {
                    result.Quantity = 1;
                }
            }
            return result;
        }
    }
}
