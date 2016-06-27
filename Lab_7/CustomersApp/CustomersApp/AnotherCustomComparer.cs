using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    public class AnotherCustomComparer : IComparer<Customer>
    {
        public int Compare(Customer x, Customer y)
        {
            return x.ID - y.ID;
        }
    }
}
