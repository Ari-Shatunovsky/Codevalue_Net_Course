using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObj
{
    public static class Extentions
    {
        public static void CopyTo(this object source, object destination)
        {
            var a = source.GetType().GetProperties().Where(p => p.CanRead && p.PropertyType.IsPublic && p.GetValue(source) != null);
            foreach (var property in a)
            {
                var destinationProperty = destination.GetType().GetProperty(property.Name);
                if (destinationProperty != null &&
                    destinationProperty.PropertyType == property.PropertyType &&
                    destinationProperty.CanWrite &&
                    destinationProperty.PropertyType.IsPublic)
                {
                    destinationProperty.SetValue(destination, property.GetValue(source));
                }
            }
        }
    }
}
