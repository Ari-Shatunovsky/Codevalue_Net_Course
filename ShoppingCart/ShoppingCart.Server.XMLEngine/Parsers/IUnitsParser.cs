using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ShoppingCart.Server.XMLEngine.Entities;

namespace ShoppingCart.Server.XMLEngine.Parsers
{
    public interface IUnitsParser
    {
        UnitsQuantity GetUnitsQuantity(XmlNode node);
    }
}
