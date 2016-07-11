using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo
{
    class AssemblyAnalyzer
    {
        public bool AnalyzeAssembly(Assembly assembly)
        {
            bool isAllApproved = true;
            Type[] assemblyTypes = assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                var attribute = type.GetCustomAttribute<CodeReviewAttribute>();
                if (attribute == null || attribute.IsApproved) continue;
                isAllApproved = false;
                break;
            }
            return isAllApproved;
        }
    }
}
