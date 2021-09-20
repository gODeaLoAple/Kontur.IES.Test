
using NUnit.Framework;

using RandomVariableTests.Tests.Calculation.Commutativity;

using RandomVariable.ExpectedValue.Operands;
using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;

using System;

namespace RandomVariableTests.Tests.Calculation
{
    [TestFixture]
    public class ExpectedValueTests : CommutativityTests<CalculatedValue>
    {
        protected override IPolishNotationCalculator<CalculatedValue> Calculator { get; }
            = new PolishNotationCalculator<CalculatedValue>(new RandomVariable.ExpectedValue.TokenParser.Parser());

        [TestCase("1", 1)]
        [TestCase("1 + 1", 2)]
        [TestCase("1 + 1 - 1", 1)]
        [TestCase("1 + 1 * 1", 2)]
        [TestCase("(1 + 1) * 1", 2)]
        [TestCase("- (1 + 1) * 1", -2)]
        [TestCase("(1 + 2) * (3 + 4) - 5", 16)]
        [TestCase("2 + 2 * 2", 6)]
        [TestCase("1d20", 10.5)]
        [TestCase("2d6+(-1d12/5)", 5.7)]
        [TestCase("(1d12+2d6)/2", 6.75)]
        [TestCase("3/2.1+100d100-4d8+1d4", 5035.928571428572)]
        public void Assert_ExpectedValue(string expression, double expected)
        {
            var notation = TokenizeAndToPolishNotation(expression);
            var actual = Calculator.Calculate(notation);
            AssertAreEqual(actual, new CalculatedValue(expected));
        }

        protected override void AssertAreEqual(CalculatedValue actual, CalculatedValue expected)
        {
            Assert.That(Math.Abs(actual.Value - expected.Value), Is.LessThan(1e-5), () => $"{actual} != {expected}");
        }

    }
}