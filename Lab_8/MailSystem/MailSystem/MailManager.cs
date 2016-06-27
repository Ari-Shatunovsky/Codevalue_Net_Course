using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSystem
{
    class MailManager
    {
        public event EventHandler<MailArrivedEventArgs> MailArrived;

        protected virtual void OnMailArrived(MailArrivedEventArgs args)
        {
            MailArrived?.Invoke(this, args);
        }

        public void SimulateMailArrived()
        {
            OnMailArrived(new MailArrivedEventArgs("Dummy Title", "Hi Dummy! \nRead this my dummy body in this dummy email.\nGo dumm yourself! \nRegards, Dummy.\n"));
        }
    }
}
