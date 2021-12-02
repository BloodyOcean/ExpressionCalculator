using Expression.Exceptions;
using Expression.Interfaces;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Models
{
    public class Equation : IEquation
    {
        private readonly List<Identificator> lst;
        private readonly Stack<decimal> stack;

        private readonly StringBuilder sb;
        public Equation(string s, params Identificator[] identificatorList)
        {
            // Initialize the identificators
            lst = new List<Identificator>();
            stack = new Stack<decimal>();

            foreach (var item in identificatorList)
            {
                lst.Add(item);
            }

            sb = new StringBuilder(s);
            Substitute();
        }

        public Equation(string s, List<Identificator> lst)
        {
            this.lst = new List<Identificator>();
            stack = new Stack<decimal>();

            sb = new StringBuilder(s);

            foreach (var item in lst)
            {
                this.lst.Add(item);
            }

            Substitute();
            
        }

        /// <summary>
        /// Replace identificators by their values
        /// </summary>
        private void Substitute()
        {
            foreach (var item in lst)
            {
                sb.Replace(item.Name, item.Value.ToString());
            }

            if (!IdentificatorsChecker.HasBadSymbols(sb.ToString()))
            {
                throw new InvalidIdentificatorException("string has unexisted identificator");
            }
        }

        /// <summary>
        /// Adds the identificators to list 
        /// and append new string to the expression
        /// </summary>
        /// <param name="s"></param>
        /// <param name="identificatorList"></param>
        public void Add(string s, params Identificator[] identificatorList)
        {
            sb.Append(s);

            foreach (var item in identificatorList)
            {
                lst.Add(item);
            }
        }

        /// <summary>
        /// Adds list of identificators to expression
        /// and append new string to expression
        /// </summary>
        /// <param name="s"></param>
        /// <param name="lst"></param>
        public void Add(string s, List<Identificator> lst)
        {
            sb.Append(s);

            foreach (var item in lst)
            {
                lst.Add(item);
            }
        }

        /// <summary>
        /// Calculate the result of expression
        /// </summary>
        /// <returns>result of calculating of decimal type</returns>
        public decimal Evaluate()
        {
            if (sb.Length == 0)
            {
                throw new EmptyEquationException("string is empty");
            }

            // Parse the expression.
            ShuntingYardParser syp = new ShuntingYardParser(sb.ToString());

            var tmp = syp.Parse();
            Console.WriteLine(String.Join(" ", tmp.ToArray()));

            decimal number;

            foreach (var token in tmp)
            {
                if (Decimal.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "^":
                            {
                                number = stack.Pop();
                                decimal degree = stack.Pop();

                                if (number == 0)
                                {
                                    stack.Push((decimal)Math.Pow((double)degree, (double)number));
                                }
                                else if ((1.0m / number) % 2 == 0 && degree < 0)
                                {   
                                    throw new ArgumentException("deegre should be a pisitive number","number");
                                }
                                else
                                {
                                    stack.Push((decimal)Math.Pow((double)degree, (double)number));
                                }

                                break;
                            }

                        case "*":
                            {
                                stack.Push(stack.Pop() * stack.Pop());
                                break;
                            }
                        case "/":
                            {
                                number = stack.Pop();

                                if (number == 0)
                                {
                                    throw new DivideByZeroException("Divide by zero");

                                }

                                stack.Push(stack.Pop() / number);
                                break;
                            }
                        case "+":
                            {
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                            }
                        case "-":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() - number);
                                break;
                            }
                        default:
                            throw new InvalidTokenException("number");
                    }

                }

            }

            return stack.Pop();
        }
    }
}
