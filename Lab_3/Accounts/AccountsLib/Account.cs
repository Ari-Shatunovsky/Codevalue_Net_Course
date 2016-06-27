
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsLib
{
    public class Account
    {
        internal Account(int id)
        {
            ID = id;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new InsufficientFundsException();
            }
        }

        public void Transfer(Account account, decimal amount)
        {
            try
            {
                Console.WriteLine("Current ballance is: {0:C2}", Balance);
                Withdraw(amount);
                account.Deposit(amount);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (InsufficientFundsException)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Transaction attemp was made");
                Console.WriteLine("Current ballance is: {0:C2}", Balance);
            }
            
        }

        public decimal ID { private set; get; }
        public decimal Balance { private set; get; }
    }
}
