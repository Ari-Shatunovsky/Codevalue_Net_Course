using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{

    

    public class Customer : IComparable<Customer>, IEquatable<Customer>
    {
        private static int currentID = 0;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Customer(string Name, string Address)
        {
            currentID ++;
            this.Name = Name;
            this.Address = Address;
            this.ID = currentID;
        }

        public int CompareTo(Customer other)
        {
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Customer other)
        {
            return string.Equals(Name.ToLower(), other.Name.ToLower()) && ID == other.ID;
        }

        public void Display()
        {
            Console.WriteLine("Customer: Name = {0}, ID = {1}, Address = {2}", Name, ID, Address);
        }
    }
}
