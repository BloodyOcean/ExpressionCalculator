using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Models
{
    public class Identificator
    {
        public string Name { get; private set; }
        public decimal Value { get; set; }

        public Identificator(string name, decimal val)
        {
            Name = name;
            Value = val;
        }
    }
}
