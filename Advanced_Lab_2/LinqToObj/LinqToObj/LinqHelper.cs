using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObj
{
    public class LinqHelper
    {
        private Process[] ProcessesCached { set; get; }

        public LinqHelper()
        {
            ProcessesCached = Process.GetProcesses();
        }

        public void PrintMsCoreLibStuff()
        {
            Console.WriteLine("MsCoreLib interfaces");
            Assembly assembly = typeof(object).Assembly;
            var interfaceQuery = assembly.DefinedTypes
                .Where(t => t.IsInterface && t.IsPublic)
                .OrderBy(t => t.Name)
                .Select(t => $"{t.Name}; methods count: {t.DeclaredMethods.Count()}");

            if (interfaceQuery.Any())
            {
                var interfaceDefinitions = interfaceQuery.ToList();
                foreach (var definition in interfaceDefinitions)
                {
                    Console.WriteLine(definition);
                }
            }
            else
            {
                Console.WriteLine("No items are found.");
            }
        }

        public void PrintProcesses()
        {
            Console.WriteLine("Processes with threads < 5");
            var processesQuery = GetOrderedProcessses();

            if (processesQuery.Any())
            {
                var processes = processesQuery.ToList();
                foreach (var process in processes)
                {
                    Console.WriteLine($"{process.ProcessName} : {process.Threads.Count}");
                }
            }
            else
            {
                Console.WriteLine("No items are found.");
            }
        }

        private ICollection<Process> GetOrderedProcessses()
        {
            var processesQuery = Process.GetProcesses()
                .Where(p => p.Threads.Count < 5)
                .OrderBy(t => t.Id);
            if (processesQuery.Any())
            {
                return processesQuery.ToList();
            }
            return new List<Process>();
        }

        public void PrintTotalThreads()
        {
            int totalThreads = ProcessesCached
                .Sum(p => p.Threads.Count);
            Console.WriteLine($"Total threads: {totalThreads}");
        }

        public void PrintProcessesGrouped()
        {
            Console.WriteLine("Processes with threads < 5 grouped by priorty");
            var processesQuery = GetOrderedProcessses()
                .GroupBy(t => t.BasePriority);

            if (processesQuery.Any())
            {
                var processesGroups = processesQuery.ToArray();
                foreach (var processesGroup in processesGroups)
                {
                    Console.WriteLine(processesGroup.Key);
                    foreach (var process in processesGroup)
                    {
                        Console.WriteLine($"{process.ProcessName } : {process.Threads.Count}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No items are found.");
            }
        }
    }
}
