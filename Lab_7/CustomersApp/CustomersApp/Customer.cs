using System;

namespace CustomersApp
{

    

    public class Customer : IComparable<Customer>, IEquatable<Customer>
    {
        private static int currentID = 0;

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
            if (other == null)
            {
                return false;
            }
            return string.Equals(Name.ToLower(), other.Name.ToLower()) && ID == other.ID;
        }

        public void Display()
        {
            Console.WriteLine("Customer: Name = {0}, ID = {1}, Address = {2}", Name, ID, Address);
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            if (other == null)
            {
                return false;
            } 
            return Equals(other);
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
