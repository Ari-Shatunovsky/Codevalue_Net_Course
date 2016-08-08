using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var xDocument = new XDocument();
            var assembly = Assembly.GetAssembly(typeof(object));
            var xml = new XElement("mscorelibClasses", assembly.DefinedTypes
                .Where(t => t.IsClass && t.IsPublic)
                .Select(t => new XElement("Type", new XAttribute("FullName", t.FullName), new XElement("Properties", t.GetProperties()
                .Where(p => p.PropertyType.IsPublic)
                .Select(p => new XElement("Property", new XAttribute("Name", p.Name), new XAttribute("Type", p.GetType())))),
                new XElement("Methods", t.GetMethods()
                .Where(m => !m.IsStatic && m.DeclaringType == t)
                .Select(m => new XElement("Method", new XAttribute("Name", m.Name), new XAttribute("ReturnType", m.ReturnType), m.GetParameters()
                .Select(param => new XElement("Parameters", new XAttribute("Name", param.Name), new XAttribute("Type", param.ParameterType))))))
                )));
            xDocument.Add(xml);
            xDocument.Save("mscorelibClasses.xml");
        }
    }
}
