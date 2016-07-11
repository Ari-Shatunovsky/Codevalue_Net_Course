using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttribDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]

    public sealed class CodeReviewAttribute : Attribute
    {
        public CodeReviewAttribute(string name, string date, bool isApproved)
        {
            Name = name;
            Date = date;
            IsApproved = isApproved;
        }

        public string Name { get; set; }
        public string Date { get; set; }
        public bool IsApproved { get; set; }
    }    
}
