using System.Collections.Generic;

namespace CustomersApp
{
    public class AnotherCustomComparer : IComparer<Customer>
    {
        public int Compare(Customer x, Customer y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            return x.ID - y.ID;
        }
    }
}
