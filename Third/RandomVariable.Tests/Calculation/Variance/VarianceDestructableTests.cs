
using NUnit.Framework;

using RandomVariableTests.Tests.Calculation.Commutativity;

using RandomVariable.PolishNotation;
using RandomVariable.PolishNotation.Interfaces;
using RandomVariable.Tokens.Entities;
using RandomVariable.Variance.Operands;

using System.Collections.Generic;

namespace RandomVariableTests.Tests.Calculation
{
    [TestFixture]
    public class VarianceDestructableTests : DestructableTests
    {
        protected IPolishNotationCalculator<CalculatedValue> Calculator { get; }
           = new PolishNotationCalculator<CalculatedValue>(new RandomVariable.Variance.TokenParser.Parser());
        protected override void Calculate(IEnumerable<Token> polishNotation)
        {
            Calculator.Calculate(polishNotation);
        }
    }
}