using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expression.Models
{
    public class ShuntingYardParser
    {
        private readonly Stack<String> ops;
        private readonly List<String> output;
        private string s;


        private static readonly Dictionary<string, (string symbol, int precedence, bool rightAssociative)> operators
        = new (string symbol, int precedence, bool rightAssociative)[] {
            ("^", 4, true),
            ("*", 3, false),
            ("/", 3, false),
            ("+", 2, false),
            ("-", 2, false)
        }.ToDictionary(op => op.symbol);

        public ShuntingYardParser(string s)
        {
            this.s = s;
            ops = new Stack<string>();
            output = new List<string>();

        }

        /// <summary>
        /// Adds spaces beetween braces and ^
        /// </summary>
        private void SeparateString()
        {
            s = s.Replace("(", " ( ");
            s = s.Replace(")", " ) ");
            s = s.Replace("^", " ^ ");
            s = s.Replace("  ", " ");
            s = s.Trim();

            Console.WriteLine(s);
        }

        /// <summary>
        /// Parsed the infix notation
        /// expression into reverse poland notation
        /// </summary>
        /// <returns>List of tokens in reverse poland notation</returns>
        public List<String> Parse()
        {
            SeparateString();

            string[] tokens = s.Split(' ');
            decimal parsedToken;

            foreach (var token in tokens)
            {
                // If it's a number
                // add it to queue.
                if (Decimal.TryParse(token, out parsedToken))
                {
                    output.Add(token);
                }
                else if (operators.TryGetValue(token, out var op1))
                {
                    while (ops.Count > 0 && operators.TryGetValue(ops.Peek(), out var op2))
                    {
                        int c = op1.precedence.CompareTo(op2.precedence);
                        if (c < 0 || !op1.rightAssociative && c <= 0)
                        {
                            output.Add(ops.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    ops.Push(token);
                }
                else if (token == "(")
                {
                    ops.Push(token);
                }
                else if (token == ")")
                {
                    string top = string.Empty;

                    while (ops.Count > 0 && (top = ops.Pop()) != "(")
                    {
                        output.Add(top);
                    }

                    if (top != "(")
                    {
                        throw new ArgumentException("No matching left parenthesis.");
                    }

                }

            }

            while (ops.Count > 0)
            {
                var top = ops.Pop();
                output.Add(top);
            }

            return output;
        }
    }
}
