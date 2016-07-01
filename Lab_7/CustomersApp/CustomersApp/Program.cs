using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomersApp
{

    internal delegate bool CustomFilter(Customer customer);

    class Program
    {
        static void Main(string[] args)
        {
            Customer[] customers = new Customer[5];
            customers[0] = new Customer("Alex", "Ramat Gan");
            customers[1] = new Customer("Zak", "Ariel");
            customers[2] = new Customer("Shirly", "Kfar Saba");
            customers[3] = new Customer("Yosi", "Haifa");
            customers[4] = new Customer("Lior", "Beer Sheva");

            Customer duplicatedCustomer = new Customer("zak", "Ariel");

            Console.WriteLine("Original array:");
            foreach (Customer customer in customers)
            {
                customer.Display();
            }

            Array.Sort(customers);

            Console.WriteLine("Sorted by internal comparer array:");
            foreach (Customer customer in customers)
            {
                customer.Display();
            }

            AnotherCustomComparer customComparer = new AnotherCustomComparer();
            Array.Sort(customers, customComparer);

            Console.WriteLine("Sorted by custom comparer array:");
            foreach (Customer customer in customers)
            {
                customer.Display();
            }

            Console.WriteLine("Checking equalty of customer:");
            duplicatedCustomer.Display();
            Console.WriteLine("With customers:");

            foreach (Customer customer in customers)
            {
                customer.Display();
                Console.WriteLine(customer.Equals(duplicatedCustomer));
            }


            //Delegates part

            CustomFilter nameAKFilter = new CustomFilter(NameFilter);
            ICollection<Customer> filteredAKCustomers = GetCustomers(customers, nameAKFilter);

            Console.WriteLine("Filtered Customers A-K:");
            foreach (Customer customer in filteredAKCustomers)
            {
                customer.Display();
            }

            ICollection<Customer> filteredLZCustomers = GetCustomers(customers, delegate (Customer customer)
            {
                return Regex.IsMatch(customer.Name, "^[l-zL-Z]");
            });

            Console.WriteLine("Filtered Customers L-Z:");
            foreach (Customer customer in filteredLZCustomers)
            {
                customer.Display();
            }


            ICollection<Customer> filteredIDCustomers = GetCustomers(customers, (Customer customer) => customer.ID < 100);

            Console.WriteLine("Filtered Customers ID < 100:");
            foreach (Customer customer in filteredIDCustomers)
            {
                customer.Display();
            }
        }

        private static bool NameFilter(Customer customer)
        {
            return Regex.IsMatch(customer.Name, "^[a-kA-K]");
        }

        static ICollection<Customer> GetCustomers(ICollection<Customer> customers, CustomFilter filter)
        {
            List<Customer> resultList = new List<Customer>();
            foreach (var customer in customers)
            {
                if (filter(customer))
                {
                    resultList.Add(customer);
                }
            }
            return resultList;
        }
    }
}
