using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsLib
{
    public class AccountFactory
    {
        private static int id;

        public static Account CreateAccount(decimal amount)
        {
            Account account = new Account(id);
            account.Deposit(amount);
            id ++;
            return account;
        } 
    }
}
