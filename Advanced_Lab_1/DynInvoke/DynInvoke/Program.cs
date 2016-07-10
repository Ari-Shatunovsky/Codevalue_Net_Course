using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = "User";

            A a = new A();
            B b = new B();
            C c = new C();
            D d = new D();
            Object o = new object();

            InvokeHello(a, userName);
            InvokeHello(b, userName);
            InvokeHello(c, userName);
            InvokeHello(d, userName);
            InvokeHello(o, userName);
            InvokeHello(60, userName);
            InvokeHello(null, userName);
        }

        static void InvokeHello(object obj, string text)
        {
            try
            {
                Type type = obj.GetType();
                object[] parameters = { text };

                MethodInfo helloMethod = type.GetMethod("Hello");
                string result = helloMethod.Invoke(obj, parameters) as string;
                Console.WriteLine(result);
            }
            catch (TargetInvocationException)
            {
                Console.WriteLine("Target class can't invoke method \"string Hello(string text)\".");
            }

            catch (TargetParameterCountException)
            {
                Console.WriteLine("Target class can't invoke method \"string Hello(string text)\". Parameters don't match");
            }

            catch (NullReferenceException)
            {
                Console.WriteLine("Target class don't have method \"string Hello(string text)\"");
            }
        }
    }
}
