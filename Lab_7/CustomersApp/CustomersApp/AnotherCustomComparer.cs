using System.Collections.Generic;

namespace CustomersApp
{
    public class AnotherCustomComparer : IComparer<Customer>
    {
        public int Compare(Customer x, Customer y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            
            return x.ID.CompareTo(y.ID);
        }
    }
}
