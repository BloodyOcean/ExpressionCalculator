using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expression;
using Expression.Models;
using Xunit;

namespace Expression.Tests
{
    public class ExpressionTests
    {
        private Equation _equation;

        [Theory]
        [InlineData("2 + 3", 5)]
        [InlineData("5 * 3", 15)]
        [InlineData("2 - 3", -1)]
        [InlineData("2 + 2", 4)]
        [InlineData("100 / 100", 1)]
        [InlineData("-8 + -8", -16)]
        public void Evaluate_SimpleValueShouldCalculate(string eq, decimal expected)
        {
            // Arrange.
            _equation = new Equation(eq, new List<Identificator>());

            // Act.
            decimal actual = _equation.Evaluate();

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("5 * (3 + 3)", 30)]
        [InlineData("5 * (3 - 3)", 0)]
        [InlineData("(10) * (3 + 3)", 60)]
        [InlineData("((8) * ((4) + (2) + (1) + (1)))", 64)]
        public void Evaluate_ShouldCalculateExpressionsWithBraces(string eq, decimal expected)
        {
            // Arrange.
            _equation = new Equation(eq, new List<Identificator>());

            // Act.
            decimal actual = _equation.Evaluate();

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Evaluate_DivideByZeroShouldReturnException()
        {
            // Arrange.
            _equation = new Equation("8 / 0", new List<Identificator>());

            // Act.

            // Assert.
            Assert.Throws<DivideByZeroException>(() => _equation.Evaluate());
        }

        [Fact]
        public void Evaluate_SqrtFromNegativeNumberShouldReturnException()
        {
            // Arrange.
            _equation = new Equation("-8^0,5", new List<Identificator>());

            // Act.

            // Assert.
            Assert.Throws<ArgumentException>("number", () => _equation.Evaluate());
        }

        [Theory]
        [InlineData("5^(1 + 1)", 25)]
        [InlineData("5^0", 1)]
        [InlineData("9^0,5", 3)]
        public void Evaluate_ShouldCalculateExpressionsWithPower(string eq, decimal expected)
        {
            // Arrange.
            _equation = new Equation(eq, new List<Identificator>());

            // Act.
            decimal actual = _equation.Evaluate();

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("((5 + 3)^2)/(2 + 1 + 1)", 16)]
        [InlineData("(1 + 256^0)^(2^2 + 48^0)", 32)]
        [InlineData("(256^(256 - 256)) / (1 / 0,25)", 0.25)]
        [InlineData("(1 / 2)^(1^0 + 1^0)", 0.25)]
        public void Evaluate_ShoulCalculaDifficultExpressions(string eq, decimal expected)
        {
            // Arrange.
            _equation = new Equation(eq, new List<Identificator>());

            // Act.
            decimal actual = _equation.Evaluate();

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
