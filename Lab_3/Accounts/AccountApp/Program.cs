using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountsLib;

namespace AccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Commands list:");
            Console.WriteLine("e - exit");
            Console.WriteLine("b - ballance");
            Console.WriteLine("d <amount> - deposit");
            Console.WriteLine("w <amount> - withdraw");
            Console.WriteLine("t <amount> - transer");

            Account accountA = AccountFactory.CreateAccount(100);
            Account accountB = AccountFactory.CreateAccount(600);

            string[] commands = new string[2];
            bool isExit = false;
            while (!isExit)
            {

                commands = Console.ReadLine().Split(' ');
                if (IsCommandCorrect(commands))
                {
                    decimal amount = 0;
                    if (commands.Length == 2)
                    {
                        decimal.TryParse(commands[1], out amount);
                    }

                    switch (commands[0])
                    {
                        case "e":
                            isExit = true;
                            break;
                        case "b":
                            Console.WriteLine("Your current ballance: {0:C2}", accountA.Balance);
                            break;
                        case "d":
                            accountA.Deposit(amount);
                            Console.WriteLine("You deposited: {0:C2}", amount);
                            break;
                        case "w":
                            try
                            {
                                accountA.Withdraw(amount);
                                Console.WriteLine("You withdrawn: {0:C2}", amount);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Amount should be positive");
                            }
                            catch (InsufficientFundsException)
                            {
                                Console.WriteLine("You have not enough money, baby!");
                            }
                            break;
                        case "t":

                            try
                            {
                                accountA.Transfer(accountB, amount);
                                Console.WriteLine("You transfered: {0:C2} to account Id {1}", amount, accountB.ID);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Amount should be positive");
                            }
                            catch (InsufficientFundsException)
                            {
                                Console.WriteLine("You have not enough money, baby!");
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Command is incorrect");
                }
                if (commands.Length == 1 && commands[0] == "e")
                {
                    isExit = true;
                }
                else if (commands.Length == 2)
                {

                }
            }
        }

        static bool IsCommandCorrect(string[] commands)
        {
            bool result;
            if (commands.Length == 1 && (commands[0] == "e" || commands[0] == "b"))
            {
                result = true;
            }
            else if (commands.Length == 2 &&
              (commands[0] == "d" ||
              commands[0] == "w" ||
              commands[0] == "t"))
            {
                decimal a;
                result = decimal.TryParse(commands[1], out a);
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
