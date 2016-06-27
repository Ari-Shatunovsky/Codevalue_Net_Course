using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            MailManager mailManager = new MailManager();
            mailManager.MailArrived += (sender, eventArgs) =>
            {
                Console.WriteLine("New e-mail!");
                Console.WriteLine(eventArgs.Title);
                Console.WriteLine(eventArgs.Body);
            };
            TimerCallback tcb = Tcb;
            Timer timer = new Timer(tcb, mailManager, 0, 1000);

            Thread.Sleep(10000);
        }

        private static void Tcb(object state)
        {
            MailManager mailManager = state as MailManager;
            mailManager?.SimulateMailArrived();
        }
    }
}
