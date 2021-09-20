
using NUnit.Framework;

using RandomVariableTests.Tests.Calculation.Commutativity;

using RandomVariable.ExpectedValue.Operands;
using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;
using RandomVariable.Tokens.Entities;

using System.Collections.Generic;

namespace RandomVariableTests.Tests.Calculation
{
    [TestFixture]
    public class ExpectedValueDestructableTests : DestructableTests
    {
        protected IPolishNotationCalculator<CalculatedValue> Calculator { get; }
            = new PolishNotationCalculator<CalculatedValue>(new RandomVariable.ExpectedValue.TokenParser.Parser());
        protected override void Calculate(IEnumerable<Token> polishNotation)
        {
            Calculator.Calculate(polishNotation);
        }
    }
}