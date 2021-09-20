
using NUnit.Framework;

using RandomVariableTests.Tests.Calculation.Commutativity;

using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;
using RandomVariable.Variance.Operands;

using System;

namespace RandomVariableTests.Tests.Calculation
{
    [TestFixture]
    public class VarianceTests : CommutativityTests<CalculatedValue>
    {
        protected override IPolishNotationCalculator<CalculatedValue> Calculator { get; }
            = new PolishNotationCalculator<CalculatedValue>(new RandomVariable.Variance.TokenParser.Parser());

        [TestCase("2 + 2 * 2", 0)]
        [TestCase("1d20", 33.25)]
        [TestCase("2d6", 5.833333333)]
        [TestCase("2d6 + 2d6", 5.833333333 * 2)]
        [TestCase("-2d6", 5.833333333)]
        [TestCase("2d6 / 5", 0.2333333)]
        [TestCase("2d6+(-1d12/5)", 6.31)]
        [TestCase("(1d12+2d6)/2", 4.4375)]
        [TestCase("100d100", 83325)]
        [TestCase("4d8", 21)]
        [TestCase("1d4", 1.25)]
        [TestCase("100d100-4d8+1d4", 83325 + 21 + 1.25)]
        [TestCase("3/2.1+100d100-4d8+1d4", 83347.25)]
        public void Assert_Variance_IsCorrect(string expression, double expected)
        {
            var actual = Calculator.Calculate(TokenizeAndToPolishNotation(expression));
            AssertAreEqual(actual, expected);
        }

        protected override void AssertAreEqual(CalculatedValue actual, CalculatedValue expected)
        {
            AssertAreEqual(actual, expected.Value);
        }

        private void AssertAreEqual(CalculatedValue actual, double expected)
        {
            var actualValue = actual.Value;
            Assert.That(Math.Abs(actualValue - expected), Is.LessThan(1e-5), () => $"{actualValue} != {expected}");
        }
    }
}