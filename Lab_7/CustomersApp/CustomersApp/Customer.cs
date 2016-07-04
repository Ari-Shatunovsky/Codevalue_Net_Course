using System;

namespace CustomersApp
{

    

    public class Customer : IComparable<Customer>, IEquatable<Customer>
    {
        //For filtering with id > 100
        private static int currentID = 98;

        public int ID { get; }
        public string Name { get; }
        public string Address { get; }

        public Customer(string name, string address)
        {
            currentID ++;
            Name = name;
            Address = address;
            ID = currentID;
        }

        public int CompareTo(Customer other)
        {
            if (other == null)
            {
                return -1;
            }
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Customer other)
        {
            return other != null && string.Equals(Name.ToLower(), other.Name.ToLower()) && ID == other.ID;
        }

        public void Display()
        {
            Console.WriteLine("Customer: Name = {0}, ID = {1}, Address = {2}", Name, ID, Address);
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + ID.GetHashCode();
            return hash;
        }
    }
}
