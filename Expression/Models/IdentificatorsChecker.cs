using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Models
{
    public static class IdentificatorsChecker
    {
        private static readonly string correctSymbols = "()1234567890+-/*^, ";

        /// <summary>
        /// Checks of string contains not existed identificators
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if correct string and false if incorrect</returns>
        public static bool HasBadSymbols(string str)
        {
            foreach (var item in str.ToCharArray())
            {
                if (!correctSymbols.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
