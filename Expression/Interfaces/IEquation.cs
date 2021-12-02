using Expression.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Interfaces
{
    interface IEquation
    {
        void Add(string s, params Identificator[] identificatorList);
        void Add(string s, List<Identificator> lst);
        decimal Evaluate();
    }
}
